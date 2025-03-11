using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Services
{
    public class HabitacionService : IHabitacionService
    {
        private readonly IHabitacionRepository _habitacionRepository;
        private readonly ILogger<HabitacionService> _logger;
        private readonly IConfiguration _configuration;

        public HabitacionService(IHabitacionRepository habitacionRepository, ILogger<HabitacionService> logger, IConfiguration configuration)
        {
            _habitacionRepository = habitacionRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetAll()
        {
            try
            {
                var habitaciones = await _habitacionRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(habitaciones, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("Hay un error aqui: ", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var habitaciones = await _habitacionRepository.GetEntityByIdAsync(id);

                if (habitaciones is null)
                    return OperationResult.GetErrorResult("Habitacion no encontrada", code: 404);

                return OperationResult.GetSuccesResult(habitaciones, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionService.GetALlById: {ex.ToString()}");
                return OperationResult.GetErrorResult("Hay un error aqui: ", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveHabitacionDto dto)
        {
            Habitacion habitacion = new()
            { 
                Numero = dto.Numero,
                Detalle = dto.Detalle,
                Precio = dto.Precio,
                Estado = dto.Estado,
                FechaCreacion = dto.FechaCreacion
            };
            try
            {
                var habitaciones = await _habitacionRepository.SaveEntityAsync(habitacion);
                if (!habitaciones.Success) throw new Exception();

                return OperationResult.GetSuccesResult(habitaciones, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurriod un error creando una nueva habitacion", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateHabitacionDto dto)
        {
            Habitacion habitacion = new()
            {
                Numero = dto.Numero,
                Detalle = dto.Detalle,
                Precio = dto.Precio,
                Estado = dto.Estado,
                FechaCreacion = dto.FechaCreacion,
                IdHabitacion = dto.IdHabitacion 
            };
            try
            {
                var habitaciones = await _habitacionRepository.UpdateEntityAsync(habitacion);
                if (!habitaciones.Success) throw new Exception();

                return OperationResult.GetSuccesResult(habitaciones, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurriod al actualizar la habitacion", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteHabitacionDto dto)
        {
            try
            {
                var habitacion = await _habitacionRepository.GetEntityByIdAsync(dto.IdHabitacion);

                if (habitacion is null)
                    return OperationResult.GetErrorResult("La habitacion con este id no existe", code: 404);

                var queryResult = await _habitacionRepository.DeleteEntityAsync(habitacion);

                if (!queryResult.Success)
                    return OperationResult.GetErrorResult("Ha ocurrido un error al eliminar esta habitacion", code: 500);

                return OperationResult.GetSuccesResult(queryResult, "Habitacion eliminada correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"HabitacionService.DeletebyId: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurrido un rrror eliminando la habitacion", code: 500);
            }
        }
    }
}
