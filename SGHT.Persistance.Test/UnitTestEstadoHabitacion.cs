using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Entities;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGHT.Persistance.Test
{
    public class UnitTestEstadoHabitacion
    {
        private readonly IEstadoHabitacionRepository _estadoHabitacionRepository;
        private readonly SGHTContext _context;

        public UnitTestEstadoHabitacion()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<EstadoHabitacionRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _estadoHabitacionRepository = new EstadoHabitacionRepository(_context, logger, configuration);
        }

        [Fact]
        public async Task CreateEstadoHabitacion_ShouldSucceed()
        {
            var estado = new EstadoHabitacion { Descripcion = "Disponible" };
            var result = await _estadoHabitacionRepository.SaveEntityAsync(estado);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetAllEstados_ShouldReturnAllEstados()
        {
            var estados = new List<EstadoHabitacion>
        {
            new EstadoHabitacion { Descripcion = "Disponible" },
            new EstadoHabitacion { Descripcion = "Ocupada" }
        };

            foreach (var estado in estados)
                await _estadoHabitacionRepository.SaveEntityAsync(estado);

            var result = await _estadoHabitacionRepository.GetAllAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}

