using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Piso;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Services
{
    public class PisoService : IPisoService
    {
        private readonly IPisoRepository _pisoRepository;
        private readonly ILogger<PisoService> _logger;
        private readonly IConfiguration _configuration;

        public PisoService(IPisoRepository pisoRepository, ILogger<PisoService> logger, IConfiguration configuration)
        {
            _pisoRepository = pisoRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var piso = await _pisoRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(piso, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var piso = await _pisoRepository.GetEntityByIdAsync(id);
                if (piso == null) return OperationResult.GetErrorResult("piso no encontrado", code: 404);

                return OperationResult.GetSuccesResult(piso);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.GetById: {ex.ToString()}");
                return OperationResult.GetErrorResult("error consultando piso", code: 500);
            }
        }

        public async Task<OperationResult> Save(SavePisoDto dto)
        {
            Piso piso = new()
            {
                Descripcion = dto.Descripcion,
               
                FechaCreacion = dto.FechaCreacion
            };

            try
            {
                var pisoo = await _pisoRepository.SaveEntityAsync(piso);
                if (!pisoo.Success) throw new Exception();

                return OperationResult.GetSuccesResult(piso, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error creando piso", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdatePisoDto dto)
        {
            Piso piso = new()
            {
                Descripcion = dto.Descripcion,
            
                FechaCreacion = dto.FechaCreacion,
                IdPiso = dto.IdPiso
            };

            try
            {
                var queryResult = await _pisoRepository.UpdateEntityAsync(piso);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.UpdateById: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error actualziando piso", code: 500);
            }
        }
        
        public async Task<OperationResult> DeleteById(DeletePisoDto dto)
        {
            try
            {
                var entity = await _pisoRepository.GetEntityByIdAsync(dto.IdPiso);
                if (entity == null) return OperationResult.GetErrorResult("piso con esa id no existe", code: 404);

                var queryResult = await _pisoRepository.DeleteEntityAsync(entity);
                if (!queryResult.Success) return OperationResult.GetErrorResult("error eliminando este piso", code: 500);

                return OperationResult.GetSuccesResult(queryResult, "Piso eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.DeletebyId: {ex.ToString()}");
                return OperationResult.GetErrorResult("Error eliminando piso", code: 500);
            }
        }
    }
}