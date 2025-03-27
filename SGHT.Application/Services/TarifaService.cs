using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Tarifa;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Services
{
    public class TarifaService : ITarifaService
    {
        private readonly ITarifasRepository _tarifasRepository;
        private readonly ILogger<TarifaService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TarifaService(ITarifasRepository tarifasRepository, ILogger<TarifaService> logger, IConfiguration configuration, IMapper mapper)
        {
            _tarifasRepository = tarifasRepository;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
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
            if (!IsDateProper(dto.FechaInicio, dto.FechaFin)) return OperationResult.GetErrorResult("la fecha de inicio tiene que ser anterior a la fecha de fin", code: 400);

            var tarifas = _mapper.Map<Tarifas>(dto);
            tarifas.Estado = "activo";

            try
            {
                var result = await _tarifasRepository.SaveEntityAsync(tarifas);

                return OperationResult.GetSuccesResult(result, "Tarifa guardada correctamente", 200);
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
            if (!IsDateProper(dto.FechaInicio, dto.FechaFin)) return OperationResult.GetErrorResult("la fecha de inicio tiene que ser anterior a la fecha de fin", code: 400);

            try
            {
                var existingTarifa = await _tarifasRepository.GetEntityByIdAsync(dto.IdTarifa);
                if (existingTarifa == null) return OperationResult.GetErrorResult("Tarifa not found", code: 404);

                var tarifas = _mapper.Map(dto, existingTarifa);

                var result = await _tarifasRepository.UpdateEntityAsync(tarifas);

                return OperationResult.GetSuccesResult(result, "Tarifa actualizada correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"TarifaService.Update: {ex}");
                return OperationResult.GetErrorResult("Error actualizando la tarifa", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteTarifaDto dto)
        {
            if (dto.IdTarifa <= 0) return OperationResult.GetErrorResult("id no es valido", code: 400);
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

        public static bool IsDateProper(DateTime fechaInicio, DateTime fechaFin)
        {
            return fechaInicio < fechaFin;
        }
    }
}
