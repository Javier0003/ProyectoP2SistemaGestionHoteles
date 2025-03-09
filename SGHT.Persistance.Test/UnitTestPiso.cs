using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Entities;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using Xunit;

namespace SGHT.Persistance.Test
{
    public class UnitTestPiso : IDisposable
    {
        private readonly IPisoRepository _pisoRepository;
        private readonly SGHTContext _context;
        private static readonly object _lock = new object();

        public UnitTestPiso()
        {
            lock (_lock)
            {
                var options = new DbContextOptionsBuilder<SGHTContext>()
                    .UseInMemoryDatabase(databaseName: $"TestPisoDB_{Guid.NewGuid()}")
                    .Options;

                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<PisoRepository>();
                var configuration = new ConfigurationBuilder().Build();

                _context = new SGHTContext(options);
                _pisoRepository = new PisoRepository(_context, logger, configuration);
            }
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreatePiso_ShouldSucceed()
        {
            // Arrange
            var piso = new Piso
            {
                
                Descripcion = "Primer piso",
                Estado = true
            };

            // Act
            var result = await _pisoRepository.SaveEntityAsync(piso);

            // Assert
            Assert.True(result.Success);
        }

       

        [Fact]
        public async Task GetPisoById_ShouldSucceed()
        {
            // Arrange
            var piso = new Piso
            {
                
                Descripcion = "Segundo piso",
                Estado = true
            };
            await _pisoRepository.SaveEntityAsync(piso);

            // Act
            var result = await _pisoRepository.GetEntityByIdAsync(piso.IdPiso);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(piso.Descripcion, result.Descripcion);
        }

        [Fact]
        public async Task GetPisoById_WithInvalidId_ShouldReturnNull()
        {
            // Act
            var result = await _pisoRepository.GetEntityByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdatePiso_ShouldSucceed()
        {
            // Arrange
            var piso = new Piso
            {

                Descripcion = "Tercer piso",
                Estado = true
            };
            await _pisoRepository.SaveEntityAsync(piso);

            piso.Descripcion = "Tercer piso actualizado";

            // Act
            var result = await _pisoRepository.UpdateEntityAsync(piso);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task DeletePiso_ShouldSetInactiveState()
        {
            // Arrange
            var piso = new Piso
            {
                IdPiso = 1,
                Descripcion = "Cuarto piso",
                Estado = true
            };
            await _pisoRepository.SaveEntityAsync(piso);

            // Act
            var deleteResult = await _pisoRepository.DeleteEntityAsync(piso);
            var deletedPiso = await _pisoRepository.GetEntityByIdAsync(piso.IdPiso);

            // Assert
            Assert.True(deleteResult.Success);
            Assert.Equal(200, deleteResult.Code);
            Assert.Null(deletedPiso); // Debe ser nulo porque el estado es inactivo
        }

        [Fact]
        public async Task DeletePiso_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var nonExistentPiso = new Piso
            {
                IdPiso = 999,
                Descripcion = "Piso 5",
                Estado = true
            };

            // Act
            var result = await _pisoRepository.DeleteEntityAsync(nonExistentPiso);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}
