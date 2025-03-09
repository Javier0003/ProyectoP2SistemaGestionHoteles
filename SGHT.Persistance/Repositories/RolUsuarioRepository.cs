using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class RolUsuarioRepository : BaseRepository<RolUsuario>, IRolUsuarioRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<RolUsuarioRepository> _logger;
        private readonly IConfiguration _configuration;
        public RolUsuarioRepository(SGHTContext context, ILogger<RolUsuarioRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<RolUsuario> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                if (result.Estado == false) return null;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> DeleteEntityAsync(RolUsuario entity)
        {
            if (entity is null) return OperationResult.GetErrorResult("Input can't be null");
            try
            {
                var result = await GetEntityByIdAsync(entity.IdRolUsuario);
                if (result is null) return OperationResult.GetErrorResult("RolUsuario no encontrado", code: 404);

                result.Estado = false;

                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se elimino correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "RolUsuario eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RolUsuarioRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando RolUsuario", code: 500);
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(RolUsuario entity)
        {
            try
            {
                var result = await base.UpdateEntityAsync(entity);
                return result.Success
                    ? OperationResult.GetSuccesResult("Actualización exitosa", code: 200)
                    : OperationResult.GetErrorResult("No se actualizo", code: 500);
            }
            catch (Exception ex)
            {
                return OperationResult.GetErrorResult($"Error al actualizar: {ex.Message}");
            }
        }

        public override async Task<OperationResult> SaveEntityAsync(RolUsuario RolUsuario)
        {
            if (RolUsuario == null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (RolUsuario.Estado == null) return OperationResult.GetErrorResult("Estado can't be null", code: 400);
            if (string.IsNullOrWhiteSpace(RolUsuario.Descripcion)) return OperationResult.GetErrorResult("Descripcion can't be null or whitespace", code: 400);

            if (_context.RolUsuarios.Any(cd => cd.Descripcion == RolUsuario.Descripcion)) return OperationResult.GetErrorResult("Esta descripcion ya esta registrado en la base de datos", code: 400);
            return await base.SaveEntityAsync(RolUsuario);
        }
    }
}
