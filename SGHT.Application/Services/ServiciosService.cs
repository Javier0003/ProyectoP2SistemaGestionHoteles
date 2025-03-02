using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.Servicio;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.Application.Services
{
    public class ServiciosService : IServiciosService
    {
        private readonly IServiciosRepository _serviciosRepository;
        private readonly ILogger<ServiciosService> _logger;
        private readonly IConfiguration _configuration;

        public ServiciosService(IServiciosRepository serviciosRepository, ILogger<ServiciosService> logger, IConfiguration configuration)
        {
            _serviciosRepository = serviciosRepository;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<OperationResult> GetAll()
        {
            try
            {
                var servicios = await _serviciosRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(servicios, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("hay un error aqui: ", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var servicios = await _serviciosRepository.GetEntityByIdAsync(id);

                if (servicios is null)
                    return OperationResult.GetErrorResult("Servicio no encontrado", code: 404);

                return OperationResult.GetSuccesResult(servicios, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosService.GetALlById: {ex.ToString()}");
                return OperationResult.GetErrorResult("Hay un error aqui: ", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveServiciosDto dto)
        {
            Servicios servicio = new()
            {
               Nombre = dto.Nombre,
               Descripcion = dto.Descripcion
            };

            try
            {
                var servicios = await _serviciosRepository.SaveEntityAsync(servicio);
                if (!servicios.Success) throw new Exception();

                return OperationResult.GetSuccesResult(servicios, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurriod un error creando un nuevo servicio", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateServiciosDto dto)
        {
            Servicios servicio = new()
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                IdServicio = dto.IdServicio
            };

            try
            {
                var servicios = await _serviciosRepository.UpdateEntityAsync(servicio);
                if (!servicios.Success) throw new Exception();

                return OperationResult.GetSuccesResult(servicios, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurriod al actualizar el servicio", code: 500);
            }
        }
        public async Task<OperationResult> DeleteById(DeleteServiciosDto dto)
        {
            try
            {
                var servicio = await _serviciosRepository.GetEntityByIdAsync(dto.IdServicio);

                if (servicio is null)
                    return OperationResult.GetErrorResult("El servicio con este id no existe", code: 404);

                var queryResult = await _serviciosRepository.DeleteEntityAsync(servicio);

                if (!queryResult.Success)
                    return OperationResult.GetErrorResult("Ha ocurrido un error al eliminar esta servicio", code: 500);

                return OperationResult.GetSuccesResult(queryResult, "Servicio eliminado correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ServiciosService.DeletebyId: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurrido un error al eliminar el servicio", code: 500);
            }
        }

    }
}
