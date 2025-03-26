using AutoMapper;
using Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using Xunit;

namespace SGHT.Application.Test
{
    public class UnitTestEstadoHabitacionService : IDisposable
    {
        private readonly Mock<IEstadoHabitacionRepository> _mockRepository;
        private readonly Mock<ILogger<EstadoHabitacionService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly EstadoHabitacionService _estadoHabitacionService;
        private readonly Mock<IMapper> _mockMapper;

        public UnitTestEstadoHabitacionService()
        {
            _mockRepository = new Mock<IEstadoHabitacionRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<EstadoHabitacionService>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMapper = new Mock<IMapper>();


            _estadoHabitacionService = new EstadoHabitacionService(
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
        public async Task GetAll_ShouldReturnAllEstados()
        {
            var estados = new List<EstadoHabitacion>
            {
                new() { IdEstadoHabitacion = 1, Descripcion = "Disponible", Estado = true },
                new() { IdEstadoHabitacion = 2, Descripcion = "Ocupado", Estado = false }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(estados);

            var result = await _estadoHabitacionService.GetAll();

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(estados, result.Data);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnEstado()
        {
            var estado = new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Disponible", Estado = true };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(estado);

            var result = await _estadoHabitacionService.GetById(1);

            Assert.True(result.Success);
            Assert.Equal(null, result.Code);
            Assert.Equal(estado, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((EstadoHabitacion)null);

            var result = await _estadoHabitacionService.GetById(999);

            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            var dto = new SaveEstadoHabitacionDto { Descripcion = "Mantenimiento", Estado = true };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<EstadoHabitacion>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            var result = await _estadoHabitacionService.Save(dto);

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task Save_WithInvalidEstado_ShouldFail()
        {
            var dto = new SaveEstadoHabitacionDto { Descripcion = "Mantenimiento", Estado = false };


            var result = await _estadoHabitacionService.Save(dto);

            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            var estado = new EstadoHabitacion { IdEstadoHabitacion = 1, Descripcion = "Reservado" };
            var dto = new DeleteEstadoHabitacionDto { IdEstadoHabitacion = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(estado);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<EstadoHabitacion>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            var result = await _estadoHabitacionService.DeleteById(dto);

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task Delete_WithNonExistentId_ShouldFail()
        {
            var dto = new DeleteEstadoHabitacionDto { IdEstadoHabitacion = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((EstadoHabitacion)null);

            var result = await _estadoHabitacionService.DeleteById(dto);

            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}