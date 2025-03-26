using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities.Reservation;
using SGHT.Persistance.Interfaces;
using Xunit;
using SGHT.Application.Utils;
using System;
using SGHT.Domain.Entities;
using AutoMapper;

namespace SGHT.Application.Test
{
    public class unitTestRecepcionService
    {

        private Mock<IRecepcionRepository> _mockRepository;
        private Mock<ILogger<RecepcionService>> _mockLogger;
        private Mock<IConfiguration> _mockConfiguration;
        private RecepcionService _recepcionService;
        private Mock<IMapper> _mockMapper;


        public unitTestRecepcionService()
        {
            _mockRepository = new Mock<IRecepcionRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<RecepcionService>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMapper = new Mock<IMapper>();


            _recepcionService = new RecepcionService(
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
        public async Task GetAll_ShouldReturnAllRecepciones()
        {
            // Arrange
            var recepciones = new List<Recepcion>
            {
                new() { IdRecepcion = 1, FechaEntrada = DateTime.Now, Estado = true },
                new() { IdRecepcion = 2, FechaEntrada = DateTime.Now, Estado = true }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(recepciones);

            // Act
            var result = await _recepcionService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(recepciones, result.Data);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnRecepcion()
        {
            // Arrange
            var recepcion = new Recepcion
            {
                IdRecepcion = 1,
                FechaEntrada = DateTime.Now,
                Estado = true
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(recepcion.IdRecepcion))
                .ReturnsAsync(recepcion);

            // Act
            var result = await _recepcionService.GetById(recepcion.IdRecepcion);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(recepcion, result.Data);
        }

        [Fact]
        public async Task GetById_WithNonFoundID_ShouldReturnError()
        {
            // Arrange
            var recepcion = new Recepcion { IdRecepcion = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(recepcion.IdRecepcion))
                .ReturnsAsync((Recepcion)null);

            // Act
            var result = await _recepcionService.GetById(recepcion.IdRecepcion);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task saveRecepcion_WithValidData_shouldSuccess()
        {
            var recepcion = new SaveRecepcionDto
            {
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5),
                FechaSalidaConfirmacion = DateTime.Now.AddDays(5),
                IdCliente = 1,
                IdHabitacion = 1,
                Estado = true,
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Recepcion>()))
               .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            var result = await _recepcionService.Save(recepcion);

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Recepcion>(t => t.Estado == recepcion.Estado)), Times.Once);
        }

        [Fact]
        public async Task saveRecepcion_WithFalseEstado_shouldFail()
        {
            var recepcion = new SaveRecepcionDto
            {
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5),
                FechaSalidaConfirmacion = DateTime.Now.AddDays(5),
                IdCliente = 1,
                IdHabitacion = 1,
                Estado = false,
            };

            var result = await _recepcionService.Save(recepcion);

            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task updateRecepcion_WithValidData_shouldSuccess()
        {
            var recepcion = new UpdateRecepcionDto
            {
                IdRecepcion = 1,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5),
                FechaSalidaConfirmacion = DateTime.Now.AddDays(5),
                IdCliente = 1,
                IdHabitacion = 1,
                Estado = true,
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Recepcion>()))
               .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            var result = await _recepcionService.UpdateById(recepcion);

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Recepcion>(t => t.Estado == recepcion.Estado)), Times.Once);
        }

        [Fact]
        public async Task updateRecepcion_WithFalseEstado_shouldFail()
        {
            var recepcion = new UpdateRecepcionDto
            {
                IdRecepcion = 1,
                FechaEntrada = DateTime.Now,
                FechaSalida = DateTime.Now.AddDays(5),
                FechaSalidaConfirmacion = DateTime.Now.AddDays(5),
                IdCliente = 1,
                IdHabitacion = 1,
                Estado = false,
            };

            var result = await _recepcionService.UpdateById(recepcion);

            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task deleteRecepcion_WithValidData_shouldSucess()
        {
            var recepcion = new Recepcion { IdRecepcion = 1, Estado = true };
            var recepciondto = new DeleteRecepcionDto { IdRecepcion = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(recepcion.IdRecepcion))
                .ReturnsAsync(recepcion);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Recepcion>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            var result = await _recepcionService.DeleteById(recepciondto);

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(recepcion), Times.Once);
        }

        [Fact]
        public async Task deleteRecepcion_WithNonFoundID_shouldFail()
        {
            var recepcion = new DeleteRecepcionDto { IdRecepcion = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(recepcion.IdRecepcion))
                .ReturnsAsync((Recepcion)null);

            var result = await _recepcionService.DeleteById(recepcion);

            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task deleteRecepcion_WithNonExistentID_shouldFail()
        {
            var recepcion = new DeleteRecepcionDto { IdRecepcion = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Recepcion)null);

            var result = await _recepcionService.DeleteById(recepcion);

            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}
