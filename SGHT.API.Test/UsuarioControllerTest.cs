using AutoMapper;
using Moq;
using SGHT.API.Controllers;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Application.Mappings; // Ensure this namespace is correct for your AutoMapperProfile
using Xunit;
using Microsoft.Extensions.Logging;
using SGHT.Persistance.Entities.Users;
using SGHT.Persistance.Interfaces;
using Microsoft.Extensions.Configuration;
using SGHT.Application.Dtos.Usuarios;
using Microsoft.EntityFrameworkCore;
using SGHT.Persistance.Context;
using SGHT.Persistance.Repositories;

namespace SGHT.API.Test
{
    public class UsuarioControllerTest : IDisposable
    {
        private readonly UsuarioController _usuarioController;
        private readonly IMapper _mapper;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<UsuarioService>> _mockLogger;

        private readonly IUsuariosRepository _usuariosRepository;
        private readonly SGHTContext _context;

        public UsuarioControllerTest()
        {
            var options = new DbContextOptionsBuilder<SGHTContext>()
                .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
                .Options;

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            var logger = loggerFactory.CreateLogger<UsuariosRepository>();
            var configuration = new ConfigurationBuilder().Build();

            _context = new SGHTContext(options);
            _usuariosRepository = new UsuariosRepository(_context, logger, configuration);

            _mockLogger = new Mock<ILogger<UsuarioService>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(c => c["Jwt:Secret"])
                .Returns("YourSuperSecretKeyWithAtLeast32Characters!!");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"])
                .Returns("SGHT.API");
            _mockConfiguration.Setup(c => c["Jwt:Audience"])
                .Returns("SGHT.Client");
            _mockConfiguration.Setup(c => c["Jwt:ExpirationInMinutes"])
                .Returns("60");

            var tokenProvider = new TokenProvider(_mockConfiguration.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>(); // Add your AutoMapper profile here
            });

            _mapper = config.CreateMapper();

            var usuarioService = new UsuarioService(_usuariosRepository, _mockLogger.Object, _mockConfiguration.Object, _mapper, tokenProvider);

            _usuarioController = new UsuarioController(usuarioService);
        }

        public void Dispose()
        {
            _mockLogger.Reset();
            _mockConfiguration.Reset();
        }

        [Fact]
        public async Task Test1()
        {
            var usuario = new SaveUsuarioDto(){
                Clave = "123456",
                Correo = "test@test.com",
                NombreCompleto = "Usuario Test",
                IdRolUsuario = 1
            };

            var users = await _usuarioController.Save(usuario);

            var result = await _usuarioController.GetAll();
            // Add assertions here
        }
    }
}