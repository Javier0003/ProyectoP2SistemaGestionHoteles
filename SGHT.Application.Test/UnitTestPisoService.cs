using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.Piso;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using Xunit;

namespace SGHT.Application.Test
{
    public class UnitTestPisoService : IDisposable
    {
        private readonly Mock<IPisoRepository> _mockRepository;
        private readonly Mock<ILogger<PisoService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly PisoService _pisoService;
        private readonly Mock<IMapper> _mockMapper;


        public UnitTestPisoService()
        {
            _mockRepository = new Mock<IPisoRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<PisoService>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMapper = new Mock<IMapper>();


            _pisoService = new PisoService(
                _mockRepository.Object,
                _mockLogger.Object,
                _mockConfiguration.Object,
                _mockMapper.Object
            );
        }

        public void Dispose()
        {
            _mockRepository.Reset();
            _mockLogger.Reset();
            _mockConfiguration.Reset();
            _mockMapper.Reset();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllPisos()
        {
            // Arrange
            var pisos = new List<Piso>
            {
                new() { IdPiso = 1, Descripcion = "Primer Piso" },
                new() { IdPiso = 2, Descripcion = "Segundo Piso" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(pisos);

            // Act
            var result = await _pisoService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(pisos, result.Data);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnPiso()
        {
            // Arrange
            var piso = new Piso { IdPiso = 1, Descripcion = "Primer Piso" };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(piso);

            // Act
            var result = await _pisoService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(null, result.Code);
            Assert.Equal(piso, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Piso)null);

            // Act
            var result = await _pisoService.GetById(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new SavePisoDto { Descripcion = "Tercer Piso" };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Piso>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _pisoService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Piso>(p =>
                p.Descripcion == dto.Descripcion)), Times.Once);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new UpdatePisoDto { IdPiso = 1, Descripcion = "Piso Actualizado" };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Piso>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _pisoService.UpdateById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Piso>(p =>
                p.IdPiso == dto.IdPiso && p.Descripcion == dto.Descripcion)), Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            // Arrange
            var piso = new Piso { IdPiso = 1, Descripcion = "Piso a eliminar" };
            var dto = new DeletePisoDto { IdPiso = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(piso);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Piso>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _pisoService.DeleteById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(piso), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var dto = new DeletePisoDto { IdPiso = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Piso)null);

            // Act
            var result = await _pisoService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}


