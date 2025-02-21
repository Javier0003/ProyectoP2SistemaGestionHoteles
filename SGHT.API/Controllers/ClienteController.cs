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
            var Usuarios = await _clienteRepository.GetAllAsync();
            return Ok(Usuarios);
        }

        [HttpGet("GetClienteById")]
        public async Task<IActionResult> Get(int Id)
        {
            var Clientes = await _clienteRepository.GetEntityByIdAsync(Id);
            return Ok(Clientes);
        }

        [HttpPatch("UpdateCliente")]
        public async Task<IActionResult> Put([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest("Cliente no puede ser nulo");

            var result = await _clienteRepository.UpdateEntityAsync(cliente);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("CreateClient")]
        public async Task<IActionResult> CreateClient([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null)
                    return BadRequest("El cliente no puede ser nulo");

                // Validaciones básicas
                if (string.IsNullOrEmpty(cliente.Documento))
                    return BadRequest("El documento es requerido");

                if (string.IsNullOrEmpty(cliente.NombreCompleto))
                    return BadRequest("El nombre es requerido");

                var result = await _clienteRepository.SaveEntityAsync(cliente);

                if (!result.Success)
                    return BadRequest(result.Message);

                return Ok(new
                {
                    Message = "Cliente creado exitosamente",
                    Cliente = cliente
                });
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("DeleteClient")]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteRepository.GetEntityByIdAsync(id);

            if (cliente == null)
                return NotFound("Cliente no encontrado");

            var result = await _clienteRepository.DeleteEntityAsync(cliente);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Cliente eliminado");
        }
    }
}
