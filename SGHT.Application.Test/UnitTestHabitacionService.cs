using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Test
{
    public class UnitTestHabitacionService
    {
        private readonly Mock<IHabitacionRepository> _mockRepository;
        private readonly Mock<ILogger<HabitacionService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly HabitacionService _habitacionService;

        public UnitTestHabitacionService()
        {
            _mockRepository = new Mock<IHabitacionRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<HabitacionService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _habitacionService = new HabitacionService(
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
        public async Task GetAll_ShouldReturnAllHabitacion()
        {
            // Arrange
            var habitacion = new List<Habitacion>
        {
            new() {
                IdHabitacion = 1,
                Numero = "221234",
                Detalle = "Exclusiva",
                Precio = 2500.00m,
                Estado = true 
            },
             new() {
                IdHabitacion = 2,
                Numero = "421234",
                Detalle = "Regular",
                Precio = 2100.00m,
                Estado = true
            },
        };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(habitacion);

            // Act
            var result = await _habitacionService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(habitacion, result.Data);
        }

        [Fact]
        public async Task GetAll_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _habitacionService.GetAll();

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

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnHabitacion()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                IdHabitacion = 1,
                Numero = "221234",
                Detalle = "Exclusiva",
                Precio = 2500.00m,
                Estado = true
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(habitacion);

            // Act
            var result = await _habitacionService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(habitacion, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Habitacion)null);

            // Act
            var result = await _habitacionService.GetById(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new SaveHabitacionDto
            {
                Numero = "221234",
                Detalle = "Exclusiva",
                Precio = 2500.00m,
                Estado = true
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Habitacion>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _habitacionService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Habitacion>(r =>
                r.Numero == dto.Numero &&
                r.Detalle == dto.Detalle &&
                r.Precio == dto.Precio &&
                r.Estado == dto.Estado)),
                Times.Once);
        }

        [Fact]
        public async Task Save_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new SaveHabitacionDto
            
            {
                Numero = "221234",
                Detalle = "Exclusiva",
                Precio = 2500.00m,
                Estado = true
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Habitacion>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _habitacionService.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new UpdateHabitacionDto
            {
                Numero = "221234",
                Detalle = "Exclusiva",
                Precio = 2500.00m,
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Habitacion>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _habitacionService.UpdateById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Habitacion>(r =>
                r.Numero == dto.Numero &&
                r.Detalle == dto.Detalle &&
                r.Precio == dto.Precio &&
                r.Estado == dto.Estado)),
                Times.Once);
        }

        [Fact]
        public async Task Update_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new UpdateHabitacionDto
            {
                Numero = "221234",
                Detalle = "Exclusiva",
                Precio = 2500.00m,
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Habitacion>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _habitacionService.UpdateById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                IdHabitacion = 1,
                Detalle = " To Delete"
            };

            var dto = new DeleteHabitacionDto { IdHabitacion  = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(habitacion);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Habitacion>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _habitacionService.DeleteById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(habitacion), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            var dto = new DeleteHabitacionDto { IdHabitacion = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Habitacion)null);

            // Act
            var result = await _habitacionService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Delete_WhenRepositoryDeleteFails_ShouldReturnError()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                IdHabitacion = 1,
                Detalle = "To Delete"
            };

            var dto = new DeleteHabitacionDto { IdHabitacion = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(habitacion);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Habitacion>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _habitacionService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

    }
}
