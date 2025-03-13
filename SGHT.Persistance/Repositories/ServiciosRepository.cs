using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class ServiciosRepository : BaseRepository<Servicios>, IServiciosRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<ServiciosRepository> _logger;
        private readonly IConfiguration _configuration;
        public ServiciosRepository(SGHTContext context, ILogger<ServiciosRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public override async Task<Servicios> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> DeleteEntityAsync(Servicios entity)
        {
            if (entity is null)
                return OperationResult.GetErrorResult("Input no puede ser null");

            try
            {
                var result = await GetEntityByIdAsync(entity.IdServicio);

                if (result is null)
                    return OperationResult.GetErrorResult("Habitacion no encontrada", code: 404);

                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se elimino correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "Servicio eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando el Servicio", code: 500);
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(Servicios entity)
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

        public override async Task<OperationResult> SaveEntityAsync(Servicios servicios)
        {
            if (servicios == null)
                return OperationResult.GetErrorResult("Body is null", code: 400);

            if (string.IsNullOrWhiteSpace(servicios.Nombre))
                return OperationResult.GetErrorResult("Nombre no puede ser null o tener espacios en blanco", code: 400);

            if (_context.Servicios.Any(cd => cd.Nombre == servicios.Nombre))
                return OperationResult.GetErrorResult("Este nombre ya esta registrado en la base de datos", code: 400);

            if (string.IsNullOrWhiteSpace(servicios.Descripcion))
                return OperationResult.GetErrorResult("Descripcion no puede ser null o tener espacios en blanco", code: 400);

            if (_context.Servicios.Any(cd => cd.Descripcion == servicios.Descripcion))
                return OperationResult.GetErrorResult("Esta descripcion ya esta registrado en la base de datos", code: 400);

            return await base.SaveEntityAsync(servicios);
        }
    }
}
