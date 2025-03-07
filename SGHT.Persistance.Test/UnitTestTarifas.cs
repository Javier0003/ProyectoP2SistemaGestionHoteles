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
    public class UnitTestTarifas : IDisposable
    {
        private readonly ITarifasRepository _tarifasRepository;
        private readonly SGHTContext _context;
        private static readonly object _lock = new object();

        public UnitTestTarifas()
        {
            lock (_lock)
            {
                var options = new DbContextOptionsBuilder<SGHTContext>()
                    .UseInMemoryDatabase(databaseName: $"TestTarifasDB_{Guid.NewGuid()}")
                    .Options;

                var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
                var logger = loggerFactory.CreateLogger<TarifasRepository>();
                var configuration = new ConfigurationBuilder().Build();

                _context = new SGHTContext(options);
                _tarifasRepository = new TarifasRepository(_context, logger, configuration);

                // Setup: Create a test habitacion
                _context.Habitaciones.Add(new Habitacion { IdHabitacion = 1, Estado = true });
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateTarifa_ShouldSucceed()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                PrecioPorNoche = 100.00M,
                Descuento = 0,
                Descripcion = "Tarifa estándar",
                Estado = "activo",
            };

            // Act
            var result = await _tarifasRepository.SaveEntityAsync(tarifa);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateTarifa_WithNullValues_ShouldFail()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 1,
                FechaInicio = null,
                FechaFin = null,
                PrecioPorNoche = null,
                Descripcion = null,
                Estado = "activo"
            };

            // Act
            var result = await _tarifasRepository.SaveEntityAsync(tarifa);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateTarifa_WithInvalidHabitacion_ShouldFail()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 999,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                PrecioPorNoche = 100.00M,
                Descripcion = "Tarifa estándar",
                Estado = "activo"
            };

            // Act
            var result = await _tarifasRepository.SaveEntityAsync(tarifa);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateTarifa_WithInvalidEstado_ShouldFail()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                PrecioPorNoche = 100.00M,
                Descripcion = "Tarifa estándar",
                Estado = "invalid"
            };

            // Act
            var result = await _tarifasRepository.SaveEntityAsync(tarifa);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task GetTarifaById_WithActiveTarifa_ShouldSucceed()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                PrecioPorNoche = 100.00M,
                Descripcion = "Tarifa para consulta",
                Estado = "activo"
            };
            await _tarifasRepository.SaveEntityAsync(tarifa);

            // Act
            var result = await _tarifasRepository.GetEntityByIdAsync(tarifa.IdTarifa);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(tarifa.Descripcion, result.Descripcion);
            Assert.Equal("activo", result.Estado);
        }

        [Fact]
        public async Task GetTarifaById_WithInactiveTarifa_ShouldReturnNull()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                PrecioPorNoche = 100.00M,
                Descripcion = "Tarifa inactiva",
                Estado = "inactivo"
            };
            await _tarifasRepository.SaveEntityAsync(tarifa);

            // Act
            var result = await _tarifasRepository.GetEntityByIdAsync(tarifa.IdTarifa);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTarifa_ShouldSucceed()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                PrecioPorNoche = 100.00M,
                Descripcion = "Tarifa original",
                Estado = "activo"
            };
            await _tarifasRepository.SaveEntityAsync(tarifa);

            tarifa.Descripcion = "Tarifa actualizada";
            tarifa.PrecioPorNoche = 120.00M;

            // Act
            var result = await _tarifasRepository.UpdateEntityAsync(tarifa);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task DeleteTarifa_ShouldSetInactiveState()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdHabitacion = 1,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                PrecioPorNoche = 100.00M,
                Descripcion = "Tarifa para eliminar",
                Estado = "activo"
            };
            await _tarifasRepository.SaveEntityAsync(tarifa);

            // Act
            var deleteResult = await _tarifasRepository.DeleteEntityAsync(tarifa);
            var deletedTarifa = await _tarifasRepository.GetEntityByIdAsync(tarifa.IdTarifa);

            // Assert
            Assert.True(deleteResult.Success);
            Assert.Equal(200, deleteResult.Code);
            Assert.Null(deletedTarifa); // Should be null because it's inactive
        }

        [Fact]
        public async Task DeleteTarifa_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var nonExistentTarifa = new Tarifas
            {
                IdTarifa = 999,
                IdHabitacion = 1,
                Descripcion = "Non Existent",
                Estado = "activo"
            };

            // Act
            var result = await _tarifasRepository.DeleteEntityAsync(nonExistentTarifa);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
} 