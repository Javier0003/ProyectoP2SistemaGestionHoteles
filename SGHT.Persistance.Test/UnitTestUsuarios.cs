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

namespace SGHT.Persistance.Test
{
    public class UnitTestUsuarios
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly SGHTContext _context;

        public UnitTestUsuarios() 
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<UsuariosRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _usuariosRepository = new UsuariosRepository(_context, logger, configuration);
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
                IdRolUsuario = 1
            };

            // Act
            var result = await _usuariosRepository.SaveEntityAsync(usuario);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUser()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Test User",
                Correo = "test@example.com",
                Clave = "password123",
                IdRolUsuario = 1
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            // Act
            var result = await _usuariosRepository.GetEntityByIdAsync(usuario.IdUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(usuario.IdUsuario, result.IdUsuario);
        }

        [Fact]
        public async Task UpdateUser_ShouldSucceed()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Test User",
                Correo = "test@example.com",
                Clave = "password123",
                IdRolUsuario = 1
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            usuario.NombreCompleto = "Updated User";
            usuario.Correo = "updated@example.com";

            // Act
            var result = await _usuariosRepository.UpdateEntityAsync(usuario);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteUser_ShouldSucceed()
        {
            // Arrange
            var usuario = new Usuarios
            {
                NombreCompleto = "Test User",
                Correo = "test@example.com",
                Clave = "password123",
                IdRolUsuario = 1
            };
            await _usuariosRepository.SaveEntityAsync(usuario);

            // Act
            var result = await _usuariosRepository.DeleteEntityAsync(usuario);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var usuarios = new List<Usuarios>
            {
                new Usuarios { NombreCompleto = "User 1", Correo = "user1@example.com", Clave = "pass1", IdRolUsuario = 1 },
                new Usuarios { NombreCompleto = "User 2", Correo = "user2@example.com", Clave = "pass2", IdRolUsuario = 2 }
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
    }
}
