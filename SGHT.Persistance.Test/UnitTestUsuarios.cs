using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using SGHT.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using SGHT.Domain.Entities;
using SGHT.Domain.Base;
using Xunit;
using SGHT.Persistance.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace SGHT.Persistance.Test
{
    public class UnitTestUsuarios : IDisposable
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly SGHTContext _context;
        private static readonly object _lock = new object();
        private static bool _databaseInitialized = false;

        public UnitTestUsuarios() 
        {
            lock (_lock)
            {
                var options = new DbContextOptionsBuilder<SGHTContext>()
                    .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                    .Options;
                
                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<UsuariosRepository>();
                var configuration = new ConfigurationBuilder().Build();

                _context = new SGHTContext(options);
                _usuariosRepository = new UsuariosRepository(_context, logger, configuration);
            }
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateUser_ShouldSucceed()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Test User",
                Correo = "test@example.com",
                Clave = "password123",
                IdRolUsuario = 1,
                FechaCreacion = DateTime.Now,
                Estado = true
            };

            // Act
            var result = await _usuariosRepository.SaveEntityAsync(usuario);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateUser_WithNullValues_ShouldFail()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = null,
                Correo = null,
                Clave = null,
                IdRolUsuario = 1,
                Estado = true
            };

            // Act
            var result = await _usuariosRepository.SaveEntityAsync(usuario);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateUser_WithDuplicateEmail_ShouldFail()
        {
            // Arrange
            var usuario1 = new Usuarios
            {
                NombreCompleto = "Test User 1",
                Correo = "duplicate@example.com",
                Clave = "password123",
                IdRolUsuario = 1,
                Estado = true
            };

            var usuario2 = new Usuarios
            {
                NombreCompleto = "Test User 2",
                Correo = "duplicate@example.com",
                Clave = "password456",
                IdRolUsuario = 1,
                Estado = true
            };

            // Act
            await _usuariosRepository.SaveEntityAsync(usuario1);
            var result = await _usuariosRepository.SaveEntityAsync(usuario2);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldSucceed()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Test User",
                Correo = "login@example.com",
                Clave = "password123",
                IdRolUsuario = 1,
                Estado = true
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            var loginRequest = new UserLogIn
            {
                Email = "login@example.com",
                Password = "password123"
            };

            // Act
            var result = await _usuariosRepository.LogIn(loginRequest);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task Login_WithInactiveUser_ShouldFail()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Inactive User",
                Correo = "inactive@example.com",
                Clave = "password123",
                IdRolUsuario = 1,
                Estado = false
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            var loginRequest = new UserLogIn
            {
                Email = "inactive@example.com",
                Password = "password123"
            };

            // Act
            var result = await _usuariosRepository.LogIn(loginRequest);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task DeleteUser_ShouldSetInactiveState()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "To Delete",
                Correo = "delete@example.com",
                Clave = "password123",
                IdRolUsuario = 1,
                Estado = true
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            // Act
            var deleteResult = await _usuariosRepository.DeleteEntityAsync(usuario);
            var deletedUser = await _usuariosRepository.GetEntityByIdAsync(usuario.IdUsuario);

            // Assert
            Assert.True(deleteResult.Success);
            Assert.Equal(200, deleteResult.Code);
            Assert.Null(deletedUser); // Because GetEntityByIdAsync returns null for inactive users
        }

        [Fact]
        public async Task UpdateUser_WithValidData_ShouldSucceed()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Original Name",
                Correo = "update@example.com",
                Clave = "password123",
                IdRolUsuario = 1,
                Estado = true
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            usuario.NombreCompleto = "Updated Name";
            usuario.Clave = "newpassword123";

            // Act
            var result = await _usuariosRepository.UpdateEntityAsync(usuario);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task GetById_WithInactiveUser_ShouldReturnNull()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Inactive User",
                Correo = "inactive2@example.com",
                Clave = "password123",
                IdRolUsuario = 1,
                Estado = false
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            // Act
            var result = await _usuariosRepository.GetEntityByIdAsync(usuario.IdUsuario);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUser_WithNullEntity_ShouldFail()
        {
            // Act
            var result = await _usuariosRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var usuarios = new List<Usuarios>
            {
                new Usuarios { NombreCompleto = "User 1", Correo = "user1@example.com", Clave = "pass1", IdRolUsuario = 1, Estado = true, FechaCreacion = DateTime.Now },
                new Usuarios { NombreCompleto = "User 2", Correo = "user2@example.com", Clave = "pass2", IdRolUsuario = 2, Estado = true, FechaCreacion = DateTime.Now }
            };

            foreach (var usuario in usuarios)
            {
                await _usuariosRepository.SaveEntityAsync(usuario);
            }

            // Act
            var result = await _usuariosRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task InserUserNull_ShouldSendError()
        {
            // Arrange
            var user = new Usuarios();

            // Act
            var result = await _usuariosRepository.SaveEntityAsync(user);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task ProperLogIn_ShouldSucceed()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "User 1",
                Correo = "test@gmail.com",
                Clave = "pass1",
                IdRolUsuario = 1,
                Estado = true,
                FechaCreacion = DateTime.Now
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            // Act
            var usuarioLogIn = new UserLogIn()
            {
                Email = "test@gmail.com",
                Password = "pass1",
            };
            var result = await _usuariosRepository.LogIn(usuarioLogIn);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task PassNulToUpdate_ShouldSendError()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "User 1",
                Correo = "test@gmail.com",
                Clave = "pass1",
                IdRolUsuario = 1,
                Estado = true,
                FechaCreacion = DateTime.Now
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            // Act
            var result = await _usuariosRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }
    }
}
