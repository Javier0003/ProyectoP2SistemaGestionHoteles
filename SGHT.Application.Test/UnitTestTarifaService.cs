using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using Xunit;

namespace SGHT.Application.Test
{
    public class UnitTestTarifaService : IDisposable
    {
        private readonly Mock<ITarifasRepository> _mockRepository;
        private readonly Mock<ILogger<TarifaService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly TarifaService _tarifaService;

        public UnitTestTarifaService()
        {
            _mockRepository = new Mock<ITarifasRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<TarifaService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _tarifaService = new TarifaService(
                _mockRepository.Object,
                _mockLogger.Object,
                _mockConfiguration.Object
            );
        }

        public void Dispose()
        {
            _mockRepository.Reset();
            _mockLogger.Reset();
            _mockConfiguration.Reset();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllTarifas()
        {
            // Arrange
            var tarifas = new List<Tarifas>
            {
                new() { IdTarifa = 1, Descripcion = "Tarifa 1", Estado = "activo" },
                new() { IdTarifa = 2, Descripcion = "Tarifa 2", Estado = "activo" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(tarifas);

            // Act
            var result = await _tarifaService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(tarifas, result.Data);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnTarifa()
        {
            // Arrange
            var tarifa = new Tarifas
            {
                IdTarifa = 1,
                Descripcion = "Test Tarifa",
                Estado = "activo"
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(tarifa);

            // Act
            var result = await _tarifaService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(tarifa, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Tarifas)null);

            // Act
            var result = await _tarifaService.GetById(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new SaveTarifaDto
            {
                Descripcion = "Nueva Tarifa",
                Estado = "activo",
                IdHabitacion = 1,
                PrecioPorNoche = 100.00M,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                Descuento = 0
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Tarifas>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _tarifaService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Tarifas>(t => 
                t.Descripcion == dto.Descripcion && 
                t.Estado == dto.Estado)), 
                Times.Once);
        }

        [Fact]
        public async Task Save_WithInvalidEstado_ShouldFail()
        {
            // Arrange
            var dto = new SaveTarifaDto
            {
                Descripcion = "Nueva Tarifa",
                Estado = "invalid",
                IdHabitacion = 1,
                PrecioPorNoche = 100.00M,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7)
            };

            // Act
            var result = await _tarifaService.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new UpdateTarifaDto
            {
                IdTarifa = 1,
                Descripcion = "Tarifa Actualizada",
                Estado = "activo",
                IdHabitacion = 1,
                PrecioPorNoche = 120.00M,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7),
                Descuento = 10
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Tarifas>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _tarifaService.UpdateById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Tarifas>(t => 
                t.IdTarifa == dto.IdTarifa && 
                t.Descripcion == dto.Descripcion)), 
                Times.Once);
        }

        [Fact]
        public async Task Update_WithInvalidEstado_ShouldFail()
        {
            // Arrange
            var dto = new UpdateTarifaDto
            {
                IdTarifa = 1,
                Descripcion = "Tarifa Actualizada",
                Estado = "invalid",
                IdHabitacion = 1,
                PrecioPorNoche = 120.00M
            };

            // Act
            var result = await _tarifaService.UpdateById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            // Arrange
            var tarifa = new Tarifas { IdTarifa = 1, Descripcion = "To Delete" };
            var dto = new DeleteTarifaDto { IdTarifa = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(tarifa);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Tarifas>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _tarifaService.DeleteById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(tarifa), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var dto = new DeleteTarifaDto { IdTarifa = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Tarifas)null);

            // Act
            var result = await _tarifaService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code); // Service returns 500 when entity is not found
        }

        [Fact]
        public async Task GetAll_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _tarifaService.GetAll();

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Save_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            var dto = new SaveTarifaDto
            {
                Descripcion = "Test Tarifa",
                Estado = "activo",
                IdHabitacion = 1,
                PrecioPorNoche = 100.00M,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddDays(7)
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Tarifas>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _tarifaService.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)),
                Times.Once);
        }
    }
} 