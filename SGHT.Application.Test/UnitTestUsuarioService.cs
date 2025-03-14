using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Services;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Entities.Users;
using SGHT.Persistance.Interfaces;
using SGHT.Application.Utils;
using AutoMapper;

namespace SGHT.Application.Test
{
    public class UnitTestUsuarioService : IDisposable
    {
        private Mock<IUsuariosRepository> _mockRepository;
        private Mock<ILogger<UsuarioService>> _mockLogger;
        private Mock<IConfiguration> _mockConfiguration;
        private UsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UnitTestUsuarioService()
        {
            SetupMocks();
        }

        private void SetupMocks()
        {
            _mockRepository = new Mock<IUsuariosRepository>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<UsuarioService>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockMapper = new Mock<IMapper>();
            
            _mockConfiguration.Setup(c => c["Jwt:Secret"])
                .Returns("YourSuperSecretKeyWithAtLeast32Characters!!");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"])
                .Returns("SGHT.API");
            _mockConfiguration.Setup(c => c["Jwt:Audience"])
                .Returns("SGHT.Client");
            _mockConfiguration.Setup(c => c["Jwt:ExpirationInMinutes"])
                .Returns("60");
                
            var tokenProvider = new TokenProvider(_mockConfiguration.Object);

            _usuarioService = new UsuarioService(
                _mockRepository.Object,
                _mockLogger.Object,
                _mockConfiguration.Object,
                tokenProvider
            );
        }

        public void Dispose()
        {
            _mockRepository.Reset();
            _mockLogger.Reset();
            _mockConfiguration.Reset();
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var usuarios = new List<Usuarios>
            {
                new() { IdUsuario = 1, NombreCompleto = "User 1" },
                new() { IdUsuario = 2, NombreCompleto = "User 2" }
            };

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(usuarios);

            // Act
            var result = await _usuarioService.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(usuarios, result.Data);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var usuario = new Usuarios
            {
                IdUsuario = 1,
                NombreCompleto = "Test User",
                Correo = "test@example.com"
            };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(usuario);

            // Act
            var result = await _usuarioService.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.Equal(usuario, result.Data);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(999))
                .ReturnsAsync(() => null);

            // Act
            var result = await _usuarioService.GetById(999);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(404, result.Code);
        }

        [Fact]
        public async Task Save_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new SaveUsuarioDto
            {
                NombreCompleto = "New User",
                Correo = "new@example.com",
                Clave = "password123",
                Estado = true,
                IdRolUsuario = 1
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Usuarios>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _usuarioService.Save(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.SaveEntityAsync(It.Is<Usuarios>(u => 
                u.NombreCompleto == dto.NombreCompleto && 
                u.Correo == dto.Correo && 
                u.IdRolUsuario == dto.IdRolUsuario)), 
                Times.Once);
        }

        [Fact]
        public async Task Update_WithValidData_ShouldSucceed()
        {
            // Arrange
            var dto = new UpdateUsuarioDto
            {
                IdUsuario = 1,
                NombreCompleto = "Updated User",
                Correo = "updated@example.com",
                Clave = "newpassword",
                Estado = true,
                IdRolUsuario = 1,
                FechaCreacion = DateTime.Now
            };

            _mockRepository.Setup(repo => repo.UpdateEntityAsync(It.IsAny<Usuarios>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _usuarioService.UpdateById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.UpdateEntityAsync(It.Is<Usuarios>(u => 
                u.IdUsuario == dto.IdUsuario && 
                u.NombreCompleto == dto.NombreCompleto)), 
                Times.Once);
        }

        [Fact]
        public async Task Delete_WithValidId_ShouldSucceed()
        {
            // Arrange
            var usuario = new Usuarios { IdUsuario = 1, NombreCompleto = "To Delete" };
            var dto = new DeleteUsuarioDto { IdUsuario = 1 };

            _mockRepository.Setup(repo => repo.GetEntityByIdAsync(1))
                .ReturnsAsync(usuario);
            _mockRepository.Setup(repo => repo.DeleteEntityAsync(It.IsAny<Usuarios>()))
                .ReturnsAsync(new OperationResult { Success = true, Code = 200 });

            // Act
            var result = await _usuarioService.DeleteById(dto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            _mockRepository.Verify(repo => repo.DeleteEntityAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldSucceed()
        {
            // Arrange
            var password = "password123";
            var hashedPassword = Passwords.HashPassword(password);

            var loginRequest = new UserLogIn
            {
                Email = "test@example.com",
                Password = password
            };

            var usuario = new Usuarios
            {
                IdUsuario = 1,
                Correo = "test@example.com",
                Clave = hashedPassword
            };

            var loginResult = new OperationResult
            {
                Success = true,
                Data = usuario,
                Code = 200
            };
            
            _mockRepository.Setup(repo => repo.LogIn(It.Is<UserLogIn>(u => 
                u.Email == loginRequest.Email)))
                .ReturnsAsync(loginResult);

            // Act
            var result = await _usuarioService.LogIn(loginRequest);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200, result.Code);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ShouldFail()
        {
            // Arrange
            var loginRequest = new UserLogIn
            {
                Email = "test@example.com",
                Password = "wrongpassword"
            };

            var usuario = new Usuarios
            {
                IdUsuario = 1,
                Correo = "test@example.com",
                Clave = Passwords.HashPassword("correctpassword")
            };

            var loginResult = new OperationResult
            {
                Success = true,
                Data = usuario,
                Code = 200
            };

            _mockRepository.Setup(repo => repo.LogIn(It.IsAny<UserLogIn>()))
                .ReturnsAsync(loginResult);

            // Act
            var result = await _usuarioService.LogIn(loginRequest);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(403, result.Code);
        }

        [Fact]
        public async Task GetAll_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _usuarioService.GetAll();

            // Assert
            Assert.False(result.Success);
            Assert.Equal(500, result.Code);
        }

        [Fact]
        public async Task Save_WhenRepositoryThrowsException_ShouldReturnError()
        {
            // Arrange
            var dto = new SaveUsuarioDto
            {
                NombreCompleto = "New User",
                Correo = "new@example.com",
                Clave = "password123"
            };

            _mockRepository.Setup(repo => repo.SaveEntityAsync(It.IsAny<Usuarios>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _usuarioService.Save(dto);

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
    }
} 