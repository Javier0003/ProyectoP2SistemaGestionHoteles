using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.ClienteDto;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities.Configuration;
using SGHT.Persistance.Interfaces;

namespace SGHT.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;
        private readonly IConfiguration _configuration;

        public ClienteService(IClienteRepository clienteRepository, ILogger<ClienteService> logger, IConfiguration configuration)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try
            {
                var clientes = await _clienteRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(clientes, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetEntityByIdAsync(id);
                return OperationResult.GetSuccesResult(cliente, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.GetById: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveClienteDto clienteDto)
        {
            Cliente cliente = new Cliente()
            {
                TipoDocumento = clienteDto.TipoDocumento,
                Documento = clienteDto.Documento,
                NombreCompleto = clienteDto.NombreCompleto,
                Correo = clienteDto.Correo,
                FechaCreacion = DateTime.Now
            };

            try
            {

                var queryResult = await _clienteRepository.SaveEntityAsync(cliente);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.Create: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateClienteDto dto)
        {
            Cliente cliente = new()
            {
                IdCliente = dto.IdCliente,
                TipoDocumento = dto.TipoDocumento,
                Documento = dto.Documento,
                NombreCompleto = dto.NombreCompleto,
                Correo = dto.Correo,
                FechaCreacion = DateTime.Now
            };

            try
            {
                var queryResult = await _clienteRepository.UpdateEntityAsync(cliente);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.Update: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteClienteDto dto)
        {
            try
            {
                var entity = await _clienteRepository.GetEntityByIdAsync(dto.IdCliente);
                if (entity == null) return OperationResult.GetErrorResult("rol con esa id no existe", code: 404);

                var queryResult = await _clienteRepository.DeleteEntityAsync(entity);
                if (!queryResult.Success) return OperationResult.GetErrorResult("error eliminando este rol", code: 500);

                return OperationResult.GetSuccesResult(queryResult, "Rol eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.Delete: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk how u got an error here", code: 500);
            }
        }
    }
}
