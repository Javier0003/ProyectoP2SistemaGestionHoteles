using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository, ILogger<ClienteController> logger)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet("GetCliente")]
        public async Task<IActionResult> Get()
        {
            var Clientes = await _clienteRepository.GetAllAsync();
            return Ok(Clientes);
        }

        [HttpGet("GetClienteById")]
        public async Task<IActionResult> Get(int Id)
        {
            var Clientes = await _clienteRepository.GetEntityByIdAsync(Id);
            return Ok(Clientes);
        }

        [HttpPost("UpdateCliente")]
        public async Task<IActionResult> Put([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente no puede ser nulo");

            var result = await _clienteRepository.UpdateEntityAsync(cliente);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
