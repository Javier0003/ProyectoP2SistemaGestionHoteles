using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using Xunit;

namespace SGHT.Persistance.Test
{
    public class UnitTestRolUsuario
    {
        private readonly IRolUsuarioRepository _rolUsuarioRepository;
        private readonly SGHTContext _context;

        public UnitTestRolUsuario()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "TestRolUsuarioDB")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<RolUsuarioRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _rolUsuarioRepository = new RolUsuarioRepository(_context, logger, configuration);
        }

        [Fact]
        public async Task CreateRolUsuario_ShouldSucceed()
        {
            // Arrange
            var rolUsuario = new RolUsuario
            {
                Descripcion = "Administrador",
                Estado = true
            };

            // Act
            var result = await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task CreateRolUsuario_WithNullValues_ShouldFail()
        {
            // Arrange
            var rolUsuario = new RolUsuario
            {
                Descripcion = null,
                Estado = null
            };

            // Act
            var result = await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateRolUsuario_WithDuplicateDescription_ShouldFail()
        {
            // Arrange
            var rolUsuario1 = new RolUsuario
            {
                Descripcion = "Administrador",
                Estado = true
            };

            var rolUsuario2 = new RolUsuario
            {
                Descripcion = "Administrador",
                Estado = true
            };

            // Act
            await _rolUsuarioRepository.SaveEntityAsync(rolUsuario1);
            var result = await _rolUsuarioRepository.SaveEntityAsync(rolUsuario2);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task GetRolUsuarioById_WithActiveRole_ShouldSucceed()
        {
            // Arrange
            var rolUsuario = new RolUsuario
            {
                Descripcion = "Gerente",
                Estado = true
            };
            await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);

            // Act
            var result = await _rolUsuarioRepository.GetEntityByIdAsync(rolUsuario.IdRolUsuario);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(rolUsuario.Descripcion, result.Descripcion);
        }

        [Fact]
        public async Task GetRolUsuarioById_WithInactiveRole_ShouldReturnNull()
        {
            // Arrange
            var rolUsuario = new RolUsuario
            {
                Descripcion = "Inactivo",
                Estado = false
            };
            await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);

            // Act
            var result = await _rolUsuarioRepository.GetEntityByIdAsync(rolUsuario.IdRolUsuario);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateRolUsuario_ShouldSucceed()
        {
            // Arrange
            var rolUsuario = new RolUsuario
            {
                Descripcion = "Original",
                Estado = true
            };
            await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);

            rolUsuario.Descripcion = "Updated";

            // Act
            var result = await _rolUsuarioRepository.UpdateEntityAsync(rolUsuario);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task UpdateRolUsuario_WithNullEntity_ShouldFail()
        {
            // Act
            var result = await _rolUsuarioRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteRolUsuario_ShouldSetInactiveState()
        {
            // Arrange
            var rolUsuario = new RolUsuario
            {
                Descripcion = "To Delete",
                Estado = true
            };
            await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);

            // Act
            var deleteResult = await _rolUsuarioRepository.DeleteEntityAsync(rolUsuario);
            var deletedRole = await _rolUsuarioRepository.GetEntityByIdAsync(rolUsuario.IdRolUsuario);

            // Assert
            Assert.True(deleteResult.Success);
            Assert.Equal(200, deleteResult.Code);
            Assert.Null(deletedRole);
        }

        [Fact]
        public async Task DeleteRolUsuario_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var nonExistentRole = new RolUsuario
            {
                IdRolUsuario = 999,
                Descripcion = "Non Existent",
                Estado = true
            };

            // Act
            var result = await _rolUsuarioRepository.DeleteEntityAsync(nonExistentRole);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
} 