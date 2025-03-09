using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class EstadoHabitacionRepository : BaseRepository<EstadoHabitacion>, IEstadoHabitacionRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<EstadoHabitacionRepository> _logger;
        private readonly IConfiguration _configuration;
        public EstadoHabitacionRepository(SGHTContext context, ILogger<EstadoHabitacionRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
    

    public override async Task<OperationResult> SaveEntityAsync(EstadoHabitacion estadoHabitacion)
        {
            if (estadoHabitacion == null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (string.IsNullOrWhiteSpace(estadoHabitacion.Descripcion)) return OperationResult.GetErrorResult("Descripcion can't be null or whitespace", code: 400);
            if (_context.EstadoHabitaciones.Any(e => e.Descripcion == estadoHabitacion.Descripcion)) return OperationResult.GetErrorResult("Este estado ya está registrado", code: 400);

            return await base.SaveEntityAsync(estadoHabitacion);
        }

        public override async Task<OperationResult> DeleteEntityAsync(EstadoHabitacion entity)
        {
            if (entity is null) return OperationResult.GetErrorResult("Input can't be null", code: 400);
            try
            {
                var result = await GetEntityByIdAsync(entity.IdEstadoHabitacion);
                if (result is null) return OperationResult.GetErrorResult("Estado de habitación no encontrado", code: 404);

                result.Estado = false;
                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se eliminó correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "Estado de habitación eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando estado de habitación", code: 500);
            }
        }

        public override async Task<EstadoHabitacion> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                return result?.Estado == false ? null : result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(EstadoHabitacion entity)
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
                return OperationResult.GetErrorResult($"Error al actualizar: {ex.Message}", code: 500);
            }
        }
    }
}
