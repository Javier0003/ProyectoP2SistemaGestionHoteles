using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
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
        public async Task<OperationResult> GetAll()
        {
            try
            {
                var data = await _usuariosRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(data, code: 200);
            }
            catch (Exception) {
                return OperationResult.GetErrorResult("idk how did u even get an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var usuario = await _usuariosRepository.GetEntityByIdAsync(id);
                if (usuario == null) return OperationResult.GetErrorResult("User not found", code: 404);
                return OperationResult.GetSuccesResult(usuario, code: 200);
            }
            catch (Exception ex) {
                _logger.LogError ($"UsuarioService.GetById: {ex}");
                return OperationResult.GetErrorResult("Error finding user", 500);
            }
        }

        public async Task<OperationResult> Save(SaveUsuarioDto dto)
        {
            try
            {
                var usuario = new Usuarios
                {
                    NombreCompleto = dto.NombreCompleto,
                    Clave = dto.Clave,
                    Correo = dto.Correo,
                    Estado = dto.Estado,
                    IdRolUsuario = dto.IdRolUsuario,
                    FechaCreacion = DateTime.Now
                };

                var result = await _usuariosRepository.SaveEntityAsync(usuario);
                return OperationResult.GetSuccesResult(result, 200 , "User created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuarioService.Save: {ex}");
                return OperationResult.GetErrorResult("Couldn't save user", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateUsuarioDto dto)
        {
            try
            {
                var usuario = new Usuarios
                {
                    IdUsuario = dto.IdUsuario,
                    NombreCompleto = dto.NombreCompleto,
                    Clave = dto.Clave,
                    Correo = dto.Correo,
                    Estado = dto.Estado,
                    IdRolUsuario = dto.IdRolUsuario,
                    FechaCreacion = dto.FechaCreacion,
                };

                var queryResult = await _usuariosRepository.UpdateEntityAsync(usuario);

                return OperationResult.GetSuccesResult(queryResult, 200 , "Usuario Actualizado Correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuarioService.UpdateById: {ex}");
                return OperationResult.GetErrorResult("Error updating user", code: 500);
            }
        }
        public async Task<OperationResult> DeleteById(DeleteUsuarioDto dto)
        {
            try
            {
                var entityToRemove = await _usuariosRepository.GetEntityByIdAsync(dto.IdUsuario);
                var result = await _usuariosRepository.DeleteEntityAsync(entityToRemove);

                return OperationResult.GetSuccesResult(result, 200, "Usuario eliminado");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UsuarioService.DeleteById: {ex}");
                return OperationResult.GetErrorResult("Error removing user", code: 500);
            }
        }
    }
}
