using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGHT.Application.Services
{
    public class EstadoHabitacionService : IEstadoHabitacionService
    {
        private readonly IEstadoHabitacionRepository _estadoHabitacionRepository;
        private readonly ILogger<EstadoHabitacionService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public EstadoHabitacionService(
            IEstadoHabitacionRepository estadoHabitacionRepository,
            ILogger<EstadoHabitacionService> logger,
            IConfiguration configuration,
            IMapper mapper)
        {
            _estadoHabitacionRepository = estadoHabitacionRepository;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var estados = await _estadoHabitacionRepository.GetAllAsync();
                var estadosDto = _mapper.Map<IEnumerable<EstadoHabitacionDto>>(estados);
                return OperationResult.GetSuccesResult(estadosDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.GetAll: {ex}");
                return OperationResult.GetErrorResult("Error obteniendo los estados de habitación", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var estado = await _estadoHabitacionRepository.GetEntityByIdAsync(id);
                if (estado == null) return OperationResult.GetErrorResult("Estado de habitación no encontrado", code: 404);

                var estadoDto = _mapper.Map<EstadoHabitacionDto>(estado);
                return OperationResult.GetSuccesResult(estadoDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.GetById: {ex}");
                return OperationResult.GetErrorResult("Error buscando estado de habitación", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveEstadoHabitacionDto dto)
        {
            try
            {
                var estado = _mapper.Map<EstadoHabitacion>(dto);
                estado.FechaCreacion = DateTime.Now;

                var result = await _estadoHabitacionRepository.SaveEntityAsync(estado);
                return OperationResult.GetSuccesResult(result, "Estado de habitación creado con éxito", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.Save: {ex}");
                return OperationResult.GetErrorResult("No se pudo guardar el estado de habitación", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateEstadoHabitacionDto dto)
        {
            try
            {
                dto.FechaCreacion = DateTime.Now;

                var estado = _mapper.Map<EstadoHabitacion>(dto);
                var queryResult = await _estadoHabitacionRepository.UpdateEntityAsync(estado);

                return OperationResult.GetSuccesResult(queryResult, "Estado de habitación actualizado correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.UpdateById: {ex}");
                return OperationResult.GetErrorResult("Error actualizando estado de habitación", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteEstadoHabitacionDto dto)
        {
            try
            {
                var entityToRemove = await _estadoHabitacionRepository.GetEntityByIdAsync(dto.IdEstadoHabitacion);
                if (entityToRemove == null) return OperationResult.GetErrorResult("Estado de habitación no encontrado", code: 404);

                var result = await _estadoHabitacionRepository.DeleteEntityAsync(entityToRemove);
                return OperationResult.GetSuccesResult(result, "Estado de habitación eliminado", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.DeleteById: {ex}");
                return OperationResult.GetErrorResult("Error eliminando estado de habitación", code: 500);
            }
        }
    }
}
