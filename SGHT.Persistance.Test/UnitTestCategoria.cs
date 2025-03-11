using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SGHT.Domain.Entities;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using Xunit;

namespace SGHT.Persistance.Test
{
    public class UnitTestCategoria
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly SGHTContext _context;

        public UnitTestCategoria()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "CategoriaDB")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<CategoriaRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _categoriaRepository = new RolUsuarioRepository(_context, logger, configuration);
        }

        [Fact]
        public async Task CreateCategoria_ShouldSucceed()
        {
            // Arrange
            var categoria = new Categoria
            {
                Descripcion = "Exclusiva",
                Estado = true,
                IdServicio = 1,
            };

            // Act
            var result = await _categoriaRepository.SaveEntityAsync(categoria);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task CreateCategoria_WithNullValues_ShouldFail()
        {
            // Arrange
           var categoria = new Categoria
            {
                Descripcion = null,
                Estado = null
            };

            // Act
            var result = await _categoriaRepository.SaveEntityAsync(categoria);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateCategoria_WithDuplicateDescription_ShouldFail()
        {
            // Arrange
            var categoria1 = new Categoria
            {
                Descripcion = "Exclusivo",
                Estado = true,
                IdServicio = 1,
            };

            var categoria2 = new Categoria
            {
                Descripcion = "Clientes",
                Estado = true,
                IdServicio = 2,
            };

            // Act
            await _categoriaRepository.SaveEntityAsync(categoria1);
            var result = await _categoriaRepository.SaveEntityAsync(categoria2);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task UpdateCategoria_ShouldSucceed()
        {
            // Arrange
            var categoria = new Categoria
            {
                Descripcion = "Clientes",
                Estado = true,
                IdServicio = 1,
            };
            await _categoriaRepository.SaveEntityAsync(categoria);

            categoria.Descripcion = "Updated";

            // Act
            var result = await _categoriaRepository.UpdateEntityAsync(categoria);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task UpdateRolUsuario_WithNullEntity_ShouldFail()
        {
            // Act
            var result = await _categoriaRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteRolUsuario_ShouldSetInactiveState()
        {
            // Arrange
            var categoria = new Categoria
            {
                Descripcion = "Ejecutivos",
                Estado = true,
                IdServicio = 1,
            };
            await _categoriaRepository.SaveEntityAsync(categoria);

            // Act
            var deleteResult = await _categoriaRepository.DeleteEntityAsync(categoria);
            var deletedRole = await _categoriaRepository.GetEntityByIdAsync(categoria.IdCategoria);

            // Assert
            Assert.True(deleteResult.Success);
            Assert.Equal(200, deleteResult.Code);
            Assert.Null(deletedRole);
        }

        [Fact]
        public async Task DeleteRolUsuario_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var categoria = new Categoria
            {
                IdCategoria = 1,
                Descripcion = "Exclusivo",
                Estado = true,
                IdServicio = 1,
            };

            // Act
            var result = await _categoriaRepository.DeleteEntityAsync(categoria);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}   
