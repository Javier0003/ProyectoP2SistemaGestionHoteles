using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class PisoRepository : BaseRepository<Piso>, IPisoRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<PisoRepository> _logger;
        private readonly IConfiguration _configuration;
        public PisoRepository(SGHTContext context, ILogger<PisoRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Piso piso)
        {
            if (piso == null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (string.IsNullOrWhiteSpace(piso.Descripcion)) return OperationResult.GetErrorResult("Descripcion can't be null or whitespace", code: 400);
            if (_context.Piso.Any(p => p.Descripcion == piso.Descripcion)) return OperationResult.GetErrorResult("Este piso ya existe", code: 400);

            return await base.SaveEntityAsync(piso);
        }

        public override async Task<OperationResult> DeleteEntityAsync(Piso entity)
        {
            if (entity is null) return OperationResult.GetErrorResult("Input can't be null");
            try
            {
                var result = await GetEntityByIdAsync(entity.IdPiso);
                if (result is null) return OperationResult.GetErrorResult("Piso no encontrado", code: 404);

                result.Estado = false;
                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se eliminó correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "Piso eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando piso", code: 500);
            }
        }

        public override async Task<Piso> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                if (result.Estado == false) return null;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(Piso entity)
        {
            try
            {
                var result = await base.UpdateEntityAsync(entity);
                return result.Success
                    ? OperationResult.GetSuccesResult("Actualización exitosa", code: 200)
                    : OperationResult.GetErrorResult("No se actualizó", code: 400);
            }
            catch (Exception ex)
            {
                return OperationResult.GetErrorResult($"Error al actualizar: {ex.Message}");
            }
        }
    }
}

