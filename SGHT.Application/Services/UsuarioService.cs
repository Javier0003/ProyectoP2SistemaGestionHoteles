using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IConfiguration _configuration;

        public UsuarioService(IUsuariosRepository usuariosRepository, ILogger<UsuarioService> logger, IConfiguration configuration)
        {
            _usuariosRepository = usuariosRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public Task<OperationResult> DeleteById(DeleteUsuarioDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(SaveUsuarioDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> UpdateById(UpdateUsuarioDto dto)
        {
            throw new NotImplementedException();
        }
    }


}
