using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using Xunit;

namespace SGHT.Application.Test
{
    public class UnitTestRolUsuarioService : IDisposable
    {
        private readonly Mock<IRolUsuarioRepository> _mockRepository;
        private readonly Mock<ILogger<RolUsuarioService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly RolUsuarioService _rolUsuarioService;
        private Mock<IMapper> _mockMapper;


        public UnitTestRolUsuarioService()
        {
            _mockRepository = new Mock<IRolUsuarioRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<RolUsuarioService>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMapper = new Mock<IMapper>();

            _rolUsuarioService = new RolUsuarioService(
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
        public async Task GetAll_ShouldReturnAllRoles()
        {
            // Arrange
            var roles = new List<RolUsuario>
            {
                new() { IdRolUsuario = 1, Descripcion = "Admin", Estado = true },
                new() { IdRolUsuario = 2, Descripcion = "User", Estado = true }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(roles);

            // Act
            var result = await _rolUsuarioService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(roles, result.Data);
        }

        [Fact]
        public async Task GetAll_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _rolUsuarioService.GetAll();

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
        public async Task GetById_WithValidId_ShouldReturnRole()
        {
            // Arrange
            var rol = new RolUsuario
            {
                IdRolUsuario = 1,
                Descripcion = "Admin",
                Estado = true
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(rol);

            // Act
            var result = await _rolUsuarioService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(rol, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((RolUsuario)null);

            // Act
            var result = await _rolUsuarioService.GetById(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new SaveRolUsuarioDto
            {
                Descripcion = "New Role",
                Estado = true
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<RolUsuario>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _rolUsuarioService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<RolUsuario>(r => 
                r.Descripcion == dto.Descripcion && 
                r.Estado == dto.Estado)), 
                Times.Once);
        }

        [Fact]
        public async Task Save_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new SaveRolUsuarioDto
            {
                Descripcion = "New Role",
                Estado = true
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<RolUsuario>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _rolUsuarioService.Save(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new UpdateRolUsuarioDto
            {
                IdRolUsuario = 1,
                Descripcion = "Updated Role",
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<RolUsuario>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _rolUsuarioService.UpdateById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<RolUsuario>(r => 
                r.IdRolUsuario == dto.IdRolUsuario && 
                r.Descripcion == dto.Descripcion)), 
                Times.Once);
        }

        [Fact]
        public async Task Update_WhenRepositoryFails_ShouldReturnError()
        {
            // Arrange
            var dto = new UpdateRolUsuarioDto
            {
                IdRolUsuario = 1,
                Descripcion = "Updated Role",
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<RolUsuario>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _rolUsuarioService.UpdateById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            // Arrange
            var rol = new RolUsuario { IdRolUsuario = 1, Descripcion = "To Delete" };
            var dto = new DeleteRolUsuarioDto { IdRolUsuario = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(rol);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<RolUsuario>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _rolUsuarioService.DeleteById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(rol), Times.Once);
        }

        [Fact]
        public async Task Delete_WithNonExistentId_ShouldReturnNotFound()
        {
            // Arrange
            var dto = new DeleteRolUsuarioDto { IdRolUsuario = 999 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync((RolUsuario)null);

            // Act
            var result = await _rolUsuarioService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Delete_WhenRepositoryDeleteFails_ShouldReturnError()
        {
            // Arrange
            var rol = new RolUsuario { IdRolUsuario = 1, Descripcion = "To Delete" };
            var dto = new DeleteRolUsuarioDto { IdRolUsuario = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(rol);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<RolUsuario>()))
                .ReturnsAsync(new OperationResult { Success = false, Code = 400 });

            // Act
            var result = await _rolUsuarioService.DeleteById(dto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }
    }
} 