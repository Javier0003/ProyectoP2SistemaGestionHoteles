using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Piso;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGHT.Application.Services
{
    public class PisoService : IPisoService
    {
        private readonly IPisoRepository _pisoRepository;
        private readonly ILogger<PisoService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PisoService(
            IPisoRepository pisoRepository,
            ILogger<PisoService> logger,
            IConfiguration configuration,
            IMapper mapper)
        {
            _pisoRepository = pisoRepository;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var pisos = await _pisoRepository.GetAllAsync();
                var pisosDto = _mapper.Map<IEnumerable<PisoDto>>(pisos);
                return OperationResult.GetSuccesResult(pisosDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.GetAll: {ex}");
                return OperationResult.GetErrorResult("Error obteniendo los pisos", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var piso = await _pisoRepository.GetEntityByIdAsync(id);
                if (piso == null) return OperationResult.GetErrorResult("Piso no encontrado", code: 404);

                var pisoDto = _mapper.Map<PisoDto>(piso);
                return OperationResult.GetSuccesResult(pisoDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.GetById: {ex}");
                return OperationResult.GetErrorResult("Error buscando el piso", code: 500);
            }
        }

        public async Task<OperationResult> Save(SavePisoDto dto)
        {
            try
            {
                var piso = _mapper.Map<Piso>(dto);
                piso.FechaCreacion = DateTime.Now;

                var result = await _pisoRepository.SaveEntityAsync(piso);
                return OperationResult.GetSuccesResult(result, "Piso creado con éxito", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.Save: {ex}");
                return OperationResult.GetErrorResult("No se pudo guardar el piso", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdatePisoDto dto)
        {
            try
            {
                dto.FechaCreacion = DateTime.Now;

                var piso = _mapper.Map<Piso>(dto);
                var queryResult = await _pisoRepository.UpdateEntityAsync(piso);

                return OperationResult.GetSuccesResult(queryResult, "Piso actualizado correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.UpdateById: {ex}");
                return OperationResult.GetErrorResult("Error actualizando el piso", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeletePisoDto dto)
        {
            try
            {
                var entityToRemove = await _pisoRepository.GetEntityByIdAsync(dto.IdPiso);
                if (entityToRemove == null) return OperationResult.GetErrorResult("Piso no encontrado", code: 404);

                var result = await _pisoRepository.DeleteEntityAsync(entityToRemove);
                return OperationResult.GetSuccesResult(result, "Piso eliminado", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"PisoService.DeleteById: {ex}");
                return OperationResult.GetErrorResult("Error eliminando el piso", code: 500);
            }
        }
    }
}
