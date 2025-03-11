using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.Servicio;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Test
{
    public class UnitTestServiciosService : IDisposable
    {
        private readonly Mock<IServiciosRepository> _mockRepository;
        private readonly Mock<ILogger<ServiciosService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly ServiciosService _serviciosService;

        public UnitTestServiciosService()
        {
            _mockRepository = new Mock<IServiciosRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<ServiciosService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _serviciosService = new ServiciosService(
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
        public async Task GetAll_ShouldReturnAllServicios()
        {
            // Arrange
            var servicios = new List<Servicios>
            {
                new() {
                    IdServicio = 1,
                    Nombre = "Comida",
                    Descripcion = "Servicios Comida"
                },
                new() {
                    IdServicio = 1,
                    Nombre = "Comida",
                    Descripcion = "Servicios Spa"
                }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(servicios);

            // Act
            var result = await _serviciosService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(servicios, result.Data);
        }

        [Fact]
        public async Task GetAll_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _serviciosService.GetAll();

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
        public async Task GetById_WithValidId_ShouldReturnServicios()
        {
            // Arrange
            var servicios = new Servicios
            {
                IdServicio = 1,
                Nombre = "Comida",
                Descripcion = "Servicios Comida"
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(servicios);

            // Act
            var result = await _serviciosService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(servicios, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Servicios)null);

            // Act
            var result = await _serviciosService.GetById(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new SaveServiciosDto
            {
                Nombre = "Comida",
                Descripcion = "Servicio Comida"
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _serviciosService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Servicios>(r =>
                r.Nombre == dto.Nombre &&
                r.Descripcion == dto.Descripcion)));
        }

        [Fact]
        public async Task Save_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new SaveServiciosDto
            {
                Nombre = "Comida",
                Descripcion = "Servicio Comida"
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _serviciosService.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new UpdateServiciosDto
            {
                IdServicio = 1,
                Nombre = "Comida",
                Descripcion = "Servicios Comida"
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _serviciosService.UpdateById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Servicios>(r =>
               r.IdServicio == dto.IdServicio && 
               r.Nombre == dto.Nombre &&
               r.Descripcion == dto.Descripcion)));
        }

        [Fact]
        public async Task Update_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new UpdateServiciosDto
            {
                IdServicio = 1,
                Nombre = "Comida",
                Descripcion = "Servicios Comida"
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _serviciosService.UpdateById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            // Arrange
            var servicios = new Servicios
            {
                IdServicio = 1,
                Nombre = "Servicios Clientes"
            };

            var dto = new DeleteServiciosDto { IdServicio = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(servicios);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _serviciosService.DeleteById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(servicios), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            var dto = new DeleteServiciosDto { IdServicio = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Servicios)null);

            // Act
            var result = await _serviciosService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Delete_WhenRepositoryDeleteFails_ShouldReturnError()
        {
            // Arrange
            var servicios = new Servicios
            {
                IdServicio = 1,
                Nombre = "Servicios Clientes",
                Descripcion = "Camarero"
            };

            var dto = new DeleteServiciosDto { IdServicio = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(servicios);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Servicios>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _serviciosService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }
    }
}
