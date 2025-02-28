using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Services
{
    public class RolUsuarioService : IRolUsuarioService
    {
        private readonly IRolUsuarioRepository _rolUsuarioRepository;
        private readonly ILogger<RolUsuarioService> _logger;
        private readonly IConfiguration _configuration;

        public RolUsuarioService(IRolUsuarioRepository rolUsuarioRepository, ILogger<RolUsuarioService> logger, IConfiguration configuration)
        {
            _rolUsuarioRepository = rolUsuarioRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var roles = await _rolUsuarioRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(roles, code: 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"RolUsuarioService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var rolUsuarios = await _rolUsuarioRepository.GetEntityByIdAsync(id);
                if (rolUsuarios == null) return OperationResult.GetErrorResult("rol no encontrado", code: 404);

                return OperationResult.GetSuccesResult(rolUsuarios, code: 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"RolUsuarioService.GetById: {ex.ToString()}");
                return OperationResult.GetErrorResult("error buscando rol", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveRolUsuarioDto dto)
        {
            RolUsuario rolUsuario = new()
            {
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                FechaCreacion = DateTime.Now
            };

            try
            {
                var rol = await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);
                if (!rol.Success) throw new Exception();

                return OperationResult.GetSuccesResult(rol, code: 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"RolUsuarioService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error creando rolUsuario", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateRolUsuarioDto dto)
        {
            RolUsuario rolUsuario = new()
            {
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                FechaCreacion = dto.FechaCreacion,
                IdRolUsuario = dto.IdRolUsuario
            };

            try
            {
                var queryResult = await _rolUsuarioRepository.UpdateEntityAsync(rolUsuario);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"RolUsuarioService.UpdateById: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error actualziando rolUsuario", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteRolUsuarioDto dto)
        {
            try
            {
                var entity = await _rolUsuarioRepository.GetEntityByIdAsync(dto.IdRolUsuario);
                if (entity == null) return OperationResult.GetErrorResult("rol con esa id no existe", code: 404);

                var queryResult = await _rolUsuarioRepository.DeleteEntityAsync(entity);
                if (!queryResult.Success) return OperationResult.GetErrorResult("error eliminando este rol", code: 500);

                return OperationResult.GetSuccesResult(queryResult,200, "Rol eliminado correctamente");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"RolUsuarioService.DeletebyId: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error eliminando rolUsuario", code: 500);
            }
        }
    }
}
