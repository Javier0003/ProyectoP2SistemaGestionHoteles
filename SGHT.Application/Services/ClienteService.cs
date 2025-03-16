using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Dtos.ClienteDto;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities.Configuration;
using SGHT.Domain.Entities.Reservation;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMapper _IMapper;

        public ClienteService(IClienteRepository clienteRepository, ILogger<ClienteService> logger, IConfiguration configuration, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
            _configuration = configuration;
            _IMapper = mapper;
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
                return OperationResult.GetErrorResult("no se encontro el cliente", code: 500);
            }
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var cliente = await _clienteRepository.GetEntityByIdAsync(id);
                if (cliente == null) return OperationResult.GetErrorResult("no se encontro un Cliente con esa Id", code: 404);

                return OperationResult.GetSuccesResult(cliente, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.GetById: {ex.ToString()}");
                return OperationResult.GetErrorResult("no se encontro el cliente", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveClienteDto clienteDto)
        {
            try
            {
                var cliente = _IMapper.Map<Cliente>(clienteDto);
                cliente.Estado = true;
                var clieDto = await _clienteRepository.SaveEntityAsync(cliente);
                if (!clieDto.Success) throw new Exception();

                return OperationResult.GetSuccesResult(clieDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.Create: {ex.ToString()}");
                return OperationResult.GetErrorResult("no se pudo guardar el cliente", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateClienteDto dto)
        {

            try
            {
                var cliente = _IMapper.Map<Cliente>(dto);
                cliente.Estado = true;
                var clieDto = await _clienteRepository.UpdateEntityAsync(cliente);
                if (!clieDto.Success) throw new Exception();

                return OperationResult.GetSuccesResult(clieDto, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.Update: {ex.ToString()}");
                return OperationResult.GetErrorResult("no se pudo actualizar el cliente", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteClienteDto dto)
        {
            try
            {
                var entity = await _clienteRepository.GetEntityByIdAsync(dto.IdCliente);
                if (entity == null) return OperationResult.GetErrorResult("Cliente con esa id no existe", code: 404);

                var result = await _clienteRepository.DeleteEntityAsync(entity);
                
                return OperationResult.GetSuccesResult(result, "Cliente eliminado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ClienteService.Delete: {ex.ToString()}");
                return OperationResult.GetErrorResult("No se pudo eliminar el cliente", code: 500);
            }
        }
    }
}
