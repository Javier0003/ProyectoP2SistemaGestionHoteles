using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.Application.Services
{
    public class TarifaService : ITarifaService
    {
        private readonly ITarifasRepository _tarifasRepository;
        private readonly ILogger<TarifaService> _logger;
        private readonly IConfiguration _configuration;

        public TarifaService(ITarifasRepository tarifasRepository, ILogger<TarifaService> logger, IConfiguration configuration)
        {
            _tarifasRepository = tarifasRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var Usuarios = await _tarifasRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(Usuarios, code: 200);
            }
            catch (Exception) 
            {
                return OperationResult.GetErrorResult("idk how did u get an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var result = await _tarifasRepository.GetEntityByIdAsync(id);
                if (result == null) return OperationResult.GetErrorResult("Tarifa not found", code: 404);

                return OperationResult.GetSuccesResult(result, code: 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"TarifaService.GetById: {ex}");
                return OperationResult.GetErrorResult("Error getting Tarifa", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveTarifaDto dto)
        {
            if (IsEstadoRight(dto.Estado)) return OperationResult.GetErrorResult("estado tiene que ser 'activo' o 'inactivo'", code: 400);
            try
            {
                Tarifas tarifas = new()
                {
                    Estado = dto.Estado,
                    Descripcion = dto.Descripcion,
                    Descuento = dto.Descuento,
                    FechaFin = dto.FechaFin,
                    FechaInicio = dto.FechaInicio,
                    IdHabitacion = dto.IdHabitacion,
                    PrecioPorNoche = dto.PrecioPorNoche,
                };

                var result = await _tarifasRepository.SaveEntityAsync(tarifas);

                return OperationResult.GetSuccesResult(result,"Tarifa guardada correctamente", 200);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"TarifaService.Save: {ex}");
                return OperationResult.GetErrorResult("Error guardando la tarifa", code: 500);
            }
        }
        public async Task<OperationResult> UpdateById(UpdateTarifaDto dto)
        {
            if (IsEstadoRight(dto.Estado)) return OperationResult.GetErrorResult("estado tiene que ser 'activo' o 'inactivo'", code: 400);
            try
            {
                Tarifas tarifas = new()
                {
                    Estado = dto.Estado,
                    Descripcion = dto.Descripcion,
                    Descuento = dto.Descuento,
                    FechaFin = dto.FechaFin,
                    FechaInicio = dto.FechaInicio,
                    IdHabitacion = dto.IdHabitacion,
                    PrecioPorNoche = dto.PrecioPorNoche,
                    IdTarifa = dto.IdTarifa
                };

                var result = await _tarifasRepository.UpdateEntityAsync(tarifas);

                return OperationResult.GetSuccesResult(result,"Tarifa guardada correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaService.Update: {ex}");
                return OperationResult.GetErrorResult("Error actualizando la tarifa", code: 500);
            }
        }
        public async Task<OperationResult> DeleteById(DeleteTarifaDto dto)
        {
            try
            {
                var entityToRemove = await _tarifasRepository.GetEntityByIdAsync(dto.IdTarifa);
                var result = await _tarifasRepository.DeleteEntityAsync(entityToRemove);

                return OperationResult.GetSuccesResult(result,"Tarifa eliminada", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaService.DeleteById: {ex}");
                return OperationResult.GetErrorResult("Error eliminando tarifa", code: 500);
            }
        }

        public static bool IsEstadoRight(string estado)
        {
            return estado != "inactivo" && estado != "activo";
        }
    }
}
