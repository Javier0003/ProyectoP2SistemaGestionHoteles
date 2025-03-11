using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Entities;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.Persistance.Test
{
    public class UnitTestHabitacion
    {
        private readonly IHabitacionRepository _habitacionRepository;
        private readonly SGHTContext _context;

        public UnitTestHabitacion()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "HabitacionDB")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<HabitacionRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _habitacionRepository = new HabitacionRepository(_context, logger, configuration);
        }

        [Fact]
        public async Task CreateServicio_ShouldSucceed()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                IdHabitacion = 1,
                Numero = "566412",
                Detalle = "Confortable",
                Precio = 2331.00m,
                Estado = true,
            };

            // Act
            var result = await _habitacionRepository.SaveEntityAsync(habitacion);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task CreateHabitacoion_WithNullValues_ShouldFail()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                Numero = null,
                Detalle = null,
                Precio = null,
                Estado = null,
            };

            // Act
            var result = await _habitacionRepository.SaveEntityAsync(habitacion);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateHabitacion_WithDuplicateDescription_ShouldFail()
        {
            // Arrange
            var habitacion1 = new Habitacion
            {
                IdHabitacion = 1, 
                Numero = "566412",
                Detalle = "Exclusiva",
                Precio = 2331.00m,
                Estado = true
            };

            var habitacion2 = new Habitacion
            {
                IdHabitacion = 1,
                Numero = "566412",
                Detalle = "Acomodada",
                Precio = 2331.00m,
                Estado = true
            };

            // Act
            await _habitacionRepository.SaveEntityAsync(habitacion1);
            var result = await _habitacionRepository.SaveEntityAsync(habitacion2);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task UpdateHabitacion_ShouldSucceed()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                IdHabitacion = 1,
                Numero = "566412",
                Detalle = "Confortable",
                Precio = 2331.00m,
                Estado = true,
            };
            await _habitacionRepository.SaveEntityAsync(habitacion);

            habitacion.Detalle = "Updated";

            // Act
            var result = await _habitacionRepository.UpdateEntityAsync(habitacion);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task UpdateHabitacion_WithNullEntity_ShouldFail()
        {
            // Act
            var result = await _habitacionRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteRolUsuario_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var habitacion = new Habitacion
            {
                IdHabitacion = 1,
                Numero = "566412",
                Detalle = "Exclusiva",
                Precio = 2331.00m,
                Estado = true,
            };

            // Act
            var result = await _habitacionRepository.DeleteEntityAsync(habitacion);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}
