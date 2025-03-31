using AutoMapper;
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
        private readonly IMapper _mapper;

        public EstadoHabitacionService(IEstadoHabitacionRepository estadoHabitacionRepository, ILogger<EstadoHabitacionService> logger, IConfiguration configuration, IMapper mapper)
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
                return OperationResult.GetSuccesResult(estados, code: 200);
            }
            catch (Exception)
            {
                return OperationResult.GetErrorResult("Error obteniendo los estados de habitación", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var result = await _estadoHabitacionRepository.GetEntityByIdAsync(id);
                if (result == null) return OperationResult.GetErrorResult("Estado de habitación no encontrado", code: 404);

                return OperationResult.GetSuccesResult(result, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.GetById: {ex}");
                return OperationResult.GetErrorResult("Error obteniendo estado de habitación", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveEstadoHabitacionDto dto)
        {
            if (!IsEstadoValid(dto.Descripcion)) return OperationResult.GetErrorResult("El estado no es válido", code: 400);

            var estado = _mapper.Map<EstadoHabitacion>(dto);

            try
            {
                var result = await _estadoHabitacionRepository.SaveEntityAsync(estado);
                return OperationResult.GetSuccesResult(result, "Estado de habitación guardado correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.Save: {ex}");
                return OperationResult.GetErrorResult("Error guardando el estado de habitación", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateEstadoHabitacionDto dto)
        {
            if (!IsEstadoValid(dto.Descripcion)) return OperationResult.GetErrorResult("El estado no es válido", code: 400);

            try
            {
                var existingEstado = await _estadoHabitacionRepository.GetEntityByIdAsync(dto.IdEstadoHabitacion);
                if (existingEstado == null) return OperationResult.GetErrorResult("Estado de habitación no encontrado", code: 404);

                var estado = _mapper.Map(dto, existingEstado);

                var result = await _estadoHabitacionRepository.UpdateEntityAsync(estado);
                return OperationResult.GetSuccesResult(result, "Estado de habitación actualizado correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"EstadoHabitacionService.Update: {ex}");
                return OperationResult.GetErrorResult("Error actualizando el estado de habitación", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteEstadoHabitacionDto dto)
        {
            if (dto.IdEstadoHabitacion <= 0) return OperationResult.GetErrorResult("ID no es válido", code: 400);
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
                return OperationResult.GetErrorResult("Error eliminando el estado de habitación", code: 500);
            }
        }

        public static bool IsEstadoValid(string estado)
        {
            var estadosValidos = new List<string> { "Disponible", "Ocupado", "Mantenimiento", "Reservado" };
            return estadosValidos.Contains(estado);
        }
    }
}
