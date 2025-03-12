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
    public class UnitTestServicios
    {
        private readonly IServiciosRepository _serviciosRepository;
        private readonly SGHTContext _context;

        public UnitTestServicios()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: "ServiciosDB")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<ServiciosRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _serviciosRepository = new ServiciosRepository(_context, logger, configuration);
        }

        [Fact]
        public async Task CreateServicio_ShouldSucceed()
        {
            // Arrange
            var servicios = new Servicios
            {
                IdServicio = 1,
                Nombre = "Food",
                Descripcion = "Servicios comida"
            };

            // Act
            var result = await _serviciosRepository.SaveEntityAsync(servicios);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task CreateServicios_WithNullValues_ShouldFail()
        {
            // Arrange
            var servicicos = new Servicios
            {
                Nombre = null,
                Descripcion = null,
            };

            // Act
            var result = await _serviciosRepository.SaveEntityAsync(servicicos);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task CreateServicios_WithDuplicateDescription_ShouldFail()
        {
            // Arrange
            var servicio1 = new Servicios
            {
                IdServicio = 1,
                Nombre = "Food",
                Descripcion = "Servicios comida"
            };

            var servicio2 = new Servicios
            {
                IdServicio = 2,
                Nombre = "Ropa",
                Descripcion = "Servicios ropa"
            };

            // Act
            await _serviciosRepository.SaveEntityAsync(servicio1);
            var result = await _serviciosRepository.SaveEntityAsync(servicio2);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(400, result.Code);
        }

        [Fact]
        public async Task UpdateServicios_ShouldSucceed()
        {
            // Arrange
            var servicios = new Servicios
            {
                IdServicio = 1,
                Nombre = "Food",
                Descripcion = "Servicios comida"
            };
            await _serviciosRepository.SaveEntityAsync(servicios);

            servicios.Descripcion = "Updated";

            // Act
            var result = await _serviciosRepository.UpdateEntityAsync(servicios);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
        }

        [Fact]
        public async Task UpdateServicios_WithNullEntity_ShouldFail()
        {
            // Act
            var result = await _serviciosRepository.UpdateEntityAsync(null);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task DeleteServicio_WithNonExistentId_ShouldFail()
        {
            // Arrange
            var servicios = new Servicios
            {
                IdServicio = 1,
                Nombre = "Food",
                Descripcion = "Servicios comida"
            };

            // Act
            var result = await _serviciosRepository.DeleteEntityAsync(servicios);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }
    }
}
