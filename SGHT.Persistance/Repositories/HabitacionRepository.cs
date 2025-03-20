using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class HabitacionRepository : BaseRepository<Habitacion>, IHabitacionRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<HabitacionRepository> _logger;
        private readonly IConfiguration _configuration;
        public HabitacionRepository(SGHTContext context, ILogger<HabitacionRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<Habitacion> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                if (result.Estado == false) return null;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> DeleteEntityAsync(Habitacion entity)
        {
            if (entity is null) 
                return OperationResult.GetErrorResult("Input no puede ser null");
            
            try
            {
                var result = await GetEntityByIdAsync(entity.IdHabitacion);

                if (result is null)
                    return OperationResult.GetErrorResult("Habitacion no encontrada", code: 404);

                result.Estado = false;

                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se elimino correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "Habitacion eliminada correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando Habitacion", code: 500);
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(Habitacion entity)
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

        public override async Task<OperationResult> SaveEntityAsync(Habitacion habitacion)
        {
            if (habitacion == null)
                return OperationResult.GetErrorResult("Body is null", code: 400);

            if (habitacion.Precio == null)
                return OperationResult.GetErrorResult("Precio no puede ser null", code: 400);

            if (habitacion.Estado == null)
                return OperationResult.GetErrorResult("Estado no puede ser null", code: 400);

            if (string.IsNullOrWhiteSpace(habitacion.Numero))
                return OperationResult.GetErrorResult("Numero no puede ser null o tener espacios en blanco", code: 400);

            if (_context.Habitaciones.Any(cd => cd.Numero == habitacion.Numero))
                return OperationResult.GetErrorResult("Este numero ya esta registrado en la base de datos", code: 400);

            if (string.IsNullOrWhiteSpace(habitacion.Detalle))
                return OperationResult.GetErrorResult("Detalle no puede ser null o tener espacios en blanco", code: 400);

            if (_context.Habitaciones.Any(cd => cd.Detalle == habitacion.Detalle))
                return OperationResult.GetErrorResult("Este detalle ya esta registrado en la base de datos", code: 400);

            return await base.SaveEntityAsync(habitacion);
        }

    }
}
