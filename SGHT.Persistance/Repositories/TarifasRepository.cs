using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class TarifasRepository : BaseRepository<Tarifas>, ITarifasRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<TarifasRepository> _logger;
        private readonly IConfiguration _configuration;
        public TarifasRepository(SGHTContext context, ILogger<TarifasRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<OperationResult> SaveEntityAsync(Tarifas tarifa)
        {
            if (tarifa == null) return OperationResult.GetErrorResult("Body is null", code: 400);
            if (tarifa.Estado != "activo") return OperationResult.GetErrorResult("Estado tiene que ser 'activo'", code: 400);
            if (string.IsNullOrEmpty(tarifa.Descripcion)) return OperationResult.GetErrorResult("Descripcion can't be null",code: 400);
            if (tarifa.FechaInicio is null) return OperationResult.GetErrorResult("Fecha inicio can't be null", code: 400);
            if (tarifa.FechaFin is null) return OperationResult.GetErrorResult("Fecha fin can't be null", code: 400);
            if (!_context.Habitaciones.Any(h => h.IdHabitacion == tarifa.IdHabitacion)) return OperationResult.GetErrorResult("La habitacion no existe", code: 400);
            if (tarifa.PrecioPorNoche is null) return OperationResult.GetErrorResult("Precio can't be null", code: 400);

            return await base.SaveEntityAsync(tarifa);
        }

        public override async Task<OperationResult> DeleteEntityAsync(Tarifas entity)
        {
            if (entity is null) return OperationResult.GetErrorResult("Input can't be null");
            try
            {
                var result = await GetEntityByIdAsync(entity.IdTarifa);
                if (result is null) return OperationResult.GetErrorResult("tarifa no encontrado", code: 404);

                result.Estado = "inactivo";

                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se elimino correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "tarifa eliminada correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifasRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando tarifa", code: 500);
            }
        }

        public override async Task<Tarifas> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                if (result.Estado == "inactivo") return null;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifasRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(Tarifas entity)
        {
            try
            {
                if (entity is null) return OperationResult.GetErrorResult("body can't be null", code: 400);
                if (string.IsNullOrWhiteSpace(entity.Estado)) return OperationResult.GetErrorResult("estado can't be null", code: 400);
                if (string.IsNullOrWhiteSpace(entity.Descripcion)) return OperationResult.GetErrorResult("descripcion can't be null", code: 400);
                if (entity.Descuento is null) return OperationResult.GetErrorResult("descuento can't be null", code: 400);
                if (entity.PrecioPorNoche is null) return OperationResult.GetErrorResult("Precio can't be null", code: 400);
                if (entity.FechaInicio is null) return OperationResult.GetErrorResult("FechaInicio can't be null", code: 400);
                if (entity.FechaFin is null) return OperationResult.GetErrorResult("FechaFin can't be null", code: 400);

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
    }
}
