using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.ClienteDto;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities.Configuration;
using SGHT.Persistance.Interfaces;
using Xunit;
using SGHT.Application.Utils;
using System;
using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Reservation;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Application.Interfaces;

namespace SGHT.Application.Test
{
    public class unitTestClienteService
    {
        public readonly Mock<IClienteRepository> _mockRepository;
        public readonly Mock<ILogger<ClienteService>> _mockLogger;
        public readonly Mock<IConfiguration> _mockConfiguration;
        public readonly ClienteService _clienteService;

        public unitTestClienteService()
        {


            _mockRepository = new Mock<IClienteRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<ClienteService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _clienteService = new ClienteService(
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
        public async Task GetAll_ShouldReturnAllClientes()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                new() { IdCliente = 1, NombreCompleto = "Cliente 1", Documento = "12345678", Estado = true },
                new() { IdCliente = 2, NombreCompleto = "Cliente 2", Documento = "87654321", Estado = true }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(clientes);

            // Act
            var result = await _clienteService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(clientes, result.Data);
        }

        [Fact]
        public async Task GetById_ShouldReturnCliente()
        {
            // Arrange
            var cliente = new Cliente
            {
                IdCliente = 1,
                NombreCompleto = "Cliente 1",
                Documento = "12345678",
                Estado = true
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(cliente);

            // Act
            var result = await _clienteService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(cliente, result.Data);
        }

        [Fact]
        public async Task SaveCliente_WithDataValid_ShouldSucceed()
        {
            // Arrange
            var cliente = new SaveClienteDto
            {
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = true,
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Cliente>()))
               .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _clienteService.Save(cliente);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Cliente>(t => t.Estado == cliente.Estado)), Times.Once);
        }

        [Fact]
        public async Task SaveCliente_WithFalseEstado_ShouldFail()
        {
            var cliente = new SaveClienteDto
            {
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = false,
            };

            var result = await _clienteService.Save(cliente);

            Assert.False(result.Success);
            Assert.Equal(500, result.Code);

        }

        [Fact]
        public async Task UpdateCliente_WithDataValid_ShouldSucceed()
        {
            // Arrange
            var cliente = new UpdateClienteDto
            {
                IdCliente = 1,
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = true,
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Cliente>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _clienteService.UpdateById(cliente);

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Cliente>(t => t.Estado == cliente.Estado)), Times.Once);

        }

        [Fact]
        public async Task updateCliente_WithEstadoFalse_ShouldFail()
        {
            var cliente = new UpdateClienteDto
            {
                IdCliente = 1,
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = false,
            };

            var result = await _clienteService.UpdateById(cliente);

            Assert.False(result.Success);
            Assert.Equal(500, result.Code);

        }

        [Fact]
        public async Task deleteClienteById_WithValidData_ShouldSuccess()
        {
            var cliente = new Cliente { IdCliente = 1, Estado = true };
            var clienteDto = new DeleteClienteDto { IdCliente = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(cliente.IdCliente))
                .ReturnsAsync(cliente);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Cliente>()))
            .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            var result = await _clienteService.DeleteById(clienteDto);

            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(cliente), Times.Once);
        }

        [Fact]
        public async Task deleteClienteById_NonFoundID_ShouldFail()
        {
            var cliente = new DeleteClienteDto { IdCliente = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(cliente.IdCliente))
                .ReturnsAsync((Cliente)null);

            var result = await _clienteService.DeleteById(cliente);

            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}
