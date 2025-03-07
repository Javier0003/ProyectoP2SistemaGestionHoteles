using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using SGHT.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Console;
using SGHT.Domain.Entities.Reservation;
using SGHT.Domain.Base;
using Xunit;

namespace SGHT.Persistance.Test
{
    public class unitTestRecepcion
    {
        private readonly IRecepcionRepository _recepcionRepository;
        private readonly SGHTContext _context;

        public unitTestRecepcion()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<RecepcionRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _recepcionRepository = new RecepcionRepository(_context, logger, configuration);
        }

        [Fact]
        public async Task CreateRecepcion_ShouldSucceed()
        {
            // Arrange
            var recepcion = new Recepcion
            {
                FechaEntrada = new DateTime(2025, 5, 10),
                FechaSalida = new DateTime(2025, 5, 20),
                FechaSalidaConfirmacion = new DateTime(2025, 5, 20),
                PrecioInicial = 100,
                Adelanto = 50,
                PrecioRestante = 50,
                TotalPagado = 50,
                CostoPenalidad = 0,
                Observacion = "Test",
                IdRecepcion = 1,
                IdHabitacion = 1,
                Estado = true,
            };

            // Act
            var result = await _recepcionRepository.SaveEntityAsync(recepcion);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateRecepcion_ShouldSucceed()
        {
            var recepcion = new Recepcion
            {
                FechaEntrada = new DateTime(2025, 5, 10),
                FechaSalida = new DateTime(2025, 5, 20),
                FechaSalidaConfirmacion = new DateTime(2025, 5, 20),
                PrecioInicial = 100,
                Adelanto = 50,
                PrecioRestante = 50,
                TotalPagado = 50,
                CostoPenalidad = 0,
                Observacion = "Test",
                IdRecepcion = 1,
                IdHabitacion = 1,
                Estado = true,
            };
            await _recepcionRepository.SaveEntityAsync(recepcion);

            recepcion.PrecioInicial = 200;
            recepcion.Observacion = "Test Update";
            recepcion.FechaEntrada = new DateTime(2025, 5, 15);

            var result = await _recepcionRepository.UpdateEntityAsync(recepcion);

            Assert.True(result.Success);
          
        }

        [Fact]
        public async Task DeleteRecepcion_ShouldSucess()
        {
            var recepcion = new Recepcion
            {
                FechaEntrada = new DateTime(2025, 5, 10),
                FechaSalida = new DateTime(2025, 5, 20),
                FechaSalidaConfirmacion = new DateTime(2025, 5, 20),
                PrecioInicial = 100,
                Adelanto = 50,
                PrecioRestante = 50,
                TotalPagado = 50,
                CostoPenalidad = 0,
                Observacion = "Test",
                IdRecepcion = 1,
                IdHabitacion = 1,
                Estado = true,
            };
            await _recepcionRepository.SaveEntityAsync(recepcion);

            var result = await _recepcionRepository.DeleteEntityAsync(recepcion);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetRecepcionByClienteID_ShouldSucceed()
        {
            var recepcion = new Recepcion
            {
                FechaEntrada = new DateTime(2025, 5, 10),
                FechaSalida = new DateTime(2025, 5, 20),
                FechaSalidaConfirmacion = new DateTime(2025, 5, 20),
                PrecioInicial = 100,
                Adelanto = 50,
                PrecioRestante = 50,
                TotalPagado = 50,
                CostoPenalidad = 0,
                Observacion = "Test",
                IdRecepcion = 1,
                IdHabitacion = 1,
                Estado = true,
            };
            await _recepcionRepository.SaveEntityAsync(recepcion);

            var result = await _recepcionRepository.GetRecepcionByClienteID(1);

            Assert.True(result.Success);
        }
    }
}
