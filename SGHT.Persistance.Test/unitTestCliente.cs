using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using SGHT.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using SGHT.Domain.Entities.Configuration;
using SGHT.Domain.Base;
using Xunit;
using SGHT.Domain.Entities.Reservation;

namespace SGHT.Persistance.Test
{
    public class unitTestCliente
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly SGHTContext _context;

        public unitTestCliente()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ClienteRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _clienteRepository = new ClienteRepository(_context, logger, configuration);
        }

        [Fact]
        public async Task CreateCliente_ShouldSucceed()
        {
            // Arrange
            var cliente = new Cliente
            {
                IdCliente = 1,
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = true,
            };

            // Act
            var result = await _clienteRepository.SaveEntityAsync(cliente);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateCliente_ShouldSucceed()
        {
            // Arrange
            var cliente = new Cliente
            {
                IdCliente = 1,
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = true,
            };

            await _clienteRepository.SaveEntityAsync(cliente);

            cliente.NombreCompleto = "Test2";

            // Act
            var result = await _clienteRepository.UpdateEntityAsync(cliente);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateCliente_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var cliente = new Cliente
            {
                IdCliente = 1,
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = true,
            };

            await _clienteRepository.SaveEntityAsync(cliente);

            cliente.NombreCompleto = "Test2";

            _context.Database.EnsureDeleted();

            // Act
            var result = await _clienteRepository.UpdateEntityAsync(cliente);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetClienteByID_ShoulSucess()
        {
            var cliente = new Cliente
            {
                IdCliente = 1,
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = true,
            };
            await _clienteRepository.SaveEntityAsync(cliente);

            var result = await _clienteRepository.GetClienteByID(1);

            Assert.True(result.Success);

        }

        [Fact]
        public async Task DeleteCliente_ShouldSucess() 
        {             
            var cliente = new Cliente
            {
                IdCliente = 1,
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test",
                Estado = true,
            };
            await _clienteRepository.SaveEntityAsync(cliente);
        
            var result = await _clienteRepository.DeleteEntityAsync(cliente);
        
            Assert.True(result.Success);
        }


        [Fact]
        public async Task createClienteWithDuplicateCorreo_ShouldError()
        {
            var cliente1 = new Cliente
            {
                TipoDocumento = "DNI",
                Documento = "12345678",
                NombreCompleto = "Test",
                Correo = "Test@example",
                Estado = true
            };
            var cliente2 = new Cliente
            {
                TipoDocumento = "DNI",
                Documento = "54565123",
                NombreCompleto = "Test2",
                Correo = "Test@example",
                Estado = true
            };

            await _clienteRepository.SaveEntityAsync(cliente1);
            var result = await _clienteRepository.SaveEntityAsync(cliente2);

            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateCliente_WithNullValues_ShouldError()
        {
            var cliente = new Cliente
            {
                TipoDocumento = null,
                Documento = null,
                NombreCompleto = null,
                Correo = null,
                Estado = false
            };

            var result = await _clienteRepository.SaveEntityAsync(cliente);

            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }
    }
}
