using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Services;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Domain.Base;
using SGHT.Application.Dtos.Categoria;


namespace SGHT.Application.Test
{
    public class UnitTestCategoriaService : IDisposable
    {
        private readonly Mock<ICategoriaRepository> _mockRepository;
        private readonly Mock<ILogger<CategoriaService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly CategoriaService _categoriaService; 

        public UnitTestCategoriaService()
        {
            _mockRepository = new Mock<ICategoriaRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<CategoriaService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _categoriaService = new CategoriaService(
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
        public async Task GetAll_ShouldReturnAllCategory()
        {
            // Arrange
            var categorias = new List<Categoria>
            {
                new() { 
                    IdCategoria = 1, 
                    Descripcion = "Regular", 
                    Estado = true, 
                    IdServicio = 1 
                },
                new() { 
                    IdCategoria = 2, 
                    Descripcion = "Premiun", 
                    Estado = true,  
                    IdServicio = 1 }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(categorias);

            // Act
            var result = await _categoriaService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(categorias, result.Data);
        }

        [Fact]
        public async Task GetAll_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _categoriaService.GetAll();

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
        public async Task GetById_WithValidId_ShouldReturnCategory()
        {
            // Arrange
            var categoria = new Categoria
            {
                IdCategoria = 1,
                Descripcion = "Admin",
                Estado = true,
                IdServicio = 1,
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(categoria);

            // Act
            var result = await _categoriaService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(categoria, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Categoria)null);

            // Act
            var result = await _categoriaService.GetById(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new SaveCategoriaDto
            {
                IdServicio = 1,
                Descripcion = "New category",
                Estado = true
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Categoria>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _categoriaService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Categoria>(r =>
                r.IdServicio == dto.IdServicio && 
                r.Descripcion == dto.Descripcion &&
                r.Estado == dto.Estado)),
                Times.Once);
        }

        [Fact]
        public async Task Save_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new SaveCategoriaDto
            {
                IdServicio = 1,
                Descripcion = "New category",
                Estado = true
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Categoria>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _categoriaService.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new UpdateCategoriaDto
            {
                IdCategoria = 1,
                Descripcion = "Updated Category",
                Estado = true,
                IdServicio = 1,
                FechaCreacion = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Categoria>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _categoriaService.UpdateById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Categoria>(r =>
                r.IdCategoria == dto.IdCategoria &&
                r.Descripcion == dto.Descripcion &&
                r.IdServicio == dto.IdServicio &&
                r.Estado == dto.Estado)),
                Times.Once);
        }

        [Fact]
        public async Task Update_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new UpdateCategoriaDto
            {
                IdCategoria = 1,
                Descripcion = "Updated Category",
                Estado = true,
                IdServicio = 1,
                FechaCreacion = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Categoria>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _categoriaService.UpdateById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            // Arrange
            var categoria = new Categoria 
            { 
                IdCategoria = 1, 
                Descripcion = "To Delete" 
            };

            var dto = new DeleteCategoriaDto { IdCategoria = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(categoria);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Categoria>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _categoriaService.DeleteById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(categoria), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            var dto = new DeleteCategoriaDto { IdCategoria = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((Categoria)null);

            // Act
            var result = await _categoriaService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Delete_WhenRepositoryDeleteFails_ShouldReturnError()
        {
            // Arrange
            var categoria = new Categoria
            {
                IdCategoria = 1,
                Descripcion = "To Delete"
            };

            var dto = new DeleteCategoriaDto { IdCategoria = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(categoria);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Categoria>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _categoriaService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }
    }
}
