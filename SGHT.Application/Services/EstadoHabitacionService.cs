using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Services
{
    public class EstadoHabitacionService : IEstadoHabitacionService
    {
        private readonly IEstadoHabitacionRepository _estadoHabitacionRepository;
        private readonly ILogger<EstadoHabitacionService> _logger;
        private readonly IConfiguration _configuration;

        public EstadoHabitacionService(IEstadoHabitacionRepository estadoHabitacionRepository, ILogger<EstadoHabitacionService> logger, IConfiguration configuration)
        {
            _estadoHabitacionRepository = estadoHabitacionRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var estado = await _estadoHabitacionRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(estado, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var estadoHabitacion = await _estadoHabitacionRepository.GetEntityByIdAsync(id);
                if (estadoHabitacion == null) return OperationResult.GetErrorResult("estado no encontrado", code: 404);

                return OperationResult.GetSuccesResult(estadoHabitacion);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.GetById: {ex.ToString()}");
                return OperationResult.GetErrorResult("error consultando estado", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveEstadoHabitacionDto dto)
        {
            EstadoHabitacion estadoHabitacion = new()
            {
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                FechaCreacion = dto.FechaCreacion
            };

            try
            {
                var estado = await _estadoHabitacionRepository.SaveEntityAsync(estadoHabitacion);
                if (!estado.Success) throw new Exception();

                return OperationResult.GetSuccesResult(estado, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error creando estadoHabitacion", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateEstadoHabitacionDto dto)
        {
            EstadoHabitacion estadoHabitacion = new()
            {
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                FechaCreacion = dto.FechaCreacion,
                IdEstadoHabitacion = dto.IdEstadoHabitacion
            };
           
            try
            {
                var queryResult = await _estadoHabitacionRepository.UpdateEntityAsync(estadoHabitacion);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.UpdateById: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error actualziando estadoHabitacion", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteEstadoHabitacionDto dto)
        {
            try
            {
                var entity = await _estadoHabitacionRepository.GetEntityByIdAsync(dto.IdEstadoHabitacion);
                if (entity == null) return OperationResult.GetErrorResult("estado con esa id no existe", code: 404);

                var queryResult = await _estadoHabitacionRepository.DeleteEntityAsync(entity);
                if (!queryResult.Success) return OperationResult.GetErrorResult("error eliminando este estado", code: 500);

                return OperationResult.GetSuccesResult(queryResult, "Estado eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.DeletebyId: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error eliminando estadoHabitacion", code: 500);
            }
        }
    }
}
