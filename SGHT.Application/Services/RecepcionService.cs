using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Reservation;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.Application.Services
{
    public class RecepcionService : IRecepcionService
    {
        private readonly IRecepcionRepository _recepcionRepository;
        private readonly ILogger<RecepcionService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RecepcionService(IRecepcionRepository recepcionRepository, ILogger<RecepcionService> logger, IConfiguration configuration, IMapper mapper)
        {
            _recepcionRepository = recepcionRepository;
            _logger = logger;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<OperationResult> DeleteById(DeleteRecepcionDto dto)
        {
            try
            {
                var entityToRemove = await _recepcionRepository.GetEntityByIdAsync(dto.IdRecepcion);
                if (entityToRemove == null) return OperationResult.GetErrorResult("Recepcion con ese ID no existe", code: 404);

                var result = await _recepcionRepository.DeleteEntityAsync(entityToRemove);

                return OperationResult.GetSuccesResult(result, "Recepcion Eliminada", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionService.DeleteById: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var clientes = await _recepcionRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(clientes, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var cliente = await _recepcionRepository.GetEntityByIdAsync(id);
                if (cliente == null) return OperationResult.GetErrorResult("Recepcion con esa id no encontrada", code: 404);

                return OperationResult.GetSuccesResult(cliente, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionService.GetById: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        

        public async Task<OperationResult> Save(SaveRecepcionDto dto)
        {
            try
            {
                var recepcion = _mapper.Map<Recepcion>(dto);
                recepcion.Estado = true;
                var recDto = await _recepcionRepository.SaveEntityAsync(recepcion);
                if (!recDto.Success) throw new Exception();

                return OperationResult.GetSuccesResult(recDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateRecepcionDto dto)
        {
           
            try
            {
                var recepcion = _mapper.Map<Recepcion>(dto);
                recepcion.Estado = true;
                var recDto = await _recepcionRepository.UpdateEntityAsync(recepcion);
                if (!recDto.Success) throw new Exception();

                return OperationResult.GetSuccesResult(recDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionService.UpdateById: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }
    }
}
