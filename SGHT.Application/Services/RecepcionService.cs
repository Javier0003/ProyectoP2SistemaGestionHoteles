using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.ClienteDto;
using SGHT.Application.Dtos.RecepcionDto;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Entities.Configuration;
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

        public RecepcionService(IRecepcionRepository recepcionRepository, ILogger<RecepcionService> logger, IConfiguration configuration)
        {
            _recepcionRepository = recepcionRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> DeleteById(DeleteRecepcionDto dto)
        {
            throw new NotImplementedException();
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
            Recepcion recepcion = new Recepcion()
            {
                    FechaEntrada = dto.FechaEntrada,
                    FechaSalida = dto.FechaSalida,
                    FechaSalidaConfirmacion = dto.FechaSalidaConfirmacion,
                    PrecioInicial = dto.PrecioInicial,
                    Adelanto = dto.Adelanto,
                    PrecioRestante = dto.PrecioRestante,
                    TotalPagado = dto.TotalPagado,
                    CostoPenalidad = dto.CostoPenalidad,
                    Observacion = dto.Observacion,
                    Estado = dto.Estado,
                    IdCliente = dto.IdCliente,
                    IdHabitacion = dto.IdHabitacion
            };

            try
            {

                var queryResult = await _recepcionRepository.SaveEntityAsync(recepcion);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionService.Create: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateRecepcionDto dto)
        {
            Recepcion recepcion = new()
            {
                IdRecepcion = dto.IdRecepcion,
                FechaEntrada = dto.FechaEntrada,
                FechaSalida = dto.FechaSalida,
                FechaSalidaConfirmacion = dto.FechaSalidaConfirmacion,
                PrecioInicial = dto.PrecioInicial,
                Adelanto = dto.Adelanto,
                PrecioRestante = dto.PrecioRestante,
                TotalPagado = dto.TotalPagado,
                CostoPenalidad = dto.CostoPenalidad,
                Observacion = dto.Observacion,
                Estado = dto.Estado,
                IdCliente = dto.IdCliente,
                IdHabitacion = dto.IdHabitacion
            };

            try
            {
                var queryResult = await _recepcionRepository.UpdateEntityAsync(recepcion);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"RecepcionService.Update: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }
    }
}
