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
    public class UnitTestEstadoHabitacion : IDisposable
    {
        private readonly IEstadoHabitacionRepository _estadoHabitacionRepository;
        private readonly SGHTContext _context;
        private static readonly object _lock = new object();

        public UnitTestEstadoHabitacion()
        {
            lock (_lock)
            {
                var options = new DbContextOptionsBuilder<SGHTContext>()
                    .UseInMemoryDatabase(databaseName: $"TestEstadoHabitacionDB_{Guid.NewGuid()}")
                    .Options;

                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<EstadoHabitacionRepository>();
                var configuration = new ConfigurationBuilder().Build();

                _context = new SGHTContext(options);
                _estadoHabitacionRepository = new EstadoHabitacionRepository(_context, logger, configuration);
            }
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateEstadoHabitacion_ShouldSucceed()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 1,
                Descripcion = "La habitación está disponible para reservar",
                Estado = true
            };

            // Act
            var result = await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateEstadoHabitacion_WithInvalidData_ShouldFail()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 0,  // ID inválido
                Descripcion = "Sin ID válido",
                Estado = true
            };

            // Act
            var result = await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task GetEstadoHabitacionById_ShouldSucceed()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 2,
                Descripcion = "La habitación está ocupada",
                Estado = true 
            };
            await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Act
            var result = await _estadoHabitacionRepository.GetEntityByIdAsync(estadoHabitacion.IdEstadoHabitacion);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(estadoHabitacion.IdEstadoHabitacion, result.IdEstadoHabitacion);
        }

        [Fact]
        public async Task GetEstadoHabitacionById_WithInvalidId_ShouldReturnNull()
        {
            // Act
            var result = await _estadoHabitacionRepository.GetEntityByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateEstadoHabitacion_ShouldSucceed()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 3,
                Descripcion = "Habitación en mantenimiento",
                Estado = true
            };
            await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            estadoHabitacion.Descripcion = "Habitación en mantenimiento preventivo";

            // Act
            var result = await _estadoHabitacionRepository.UpdateEntityAsync(estadoHabitacion);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task DeleteEstadoHabitacion_ShouldSetInactiveState()
        {
            // Arrange
            var estadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 4,
                Descripcion = "La habitación está reservada",
                Estado = true
            };
            await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);

            // Act
            var deleteResult = await _estadoHabitacionRepository.DeleteEntityAsync(estadoHabitacion);
            var deletedEstadoHabitacion = await _estadoHabitacionRepository.GetEntityByIdAsync(estadoHabitacion.IdEstadoHabitacion);

            // Assert
            Assert.True(deleteResult.Success);
            Assert.Equal(200, deleteResult.Code);
            Assert.Null(deletedEstadoHabitacion); // Debe ser nulo porque el estado es inactivo
        }

        [Fact]
        public async Task DeleteEstadoHabitacion_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var nonExistentEstadoHabitacion = new EstadoHabitacion
            {
                IdEstadoHabitacion = 999,
                Descripcion = "No debería existir",
                Estado = true
            };

            // Act
            var result = await _estadoHabitacionRepository.DeleteEntityAsync(nonExistentEstadoHabitacion);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}
