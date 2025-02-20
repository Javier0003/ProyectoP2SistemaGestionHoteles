using Microsoft.AspNetCore.Mvc;
<<<<<<< Updated upstream
using SGHT.Persistance.Interfaces;
=======
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces.Configuration;
>>>>>>> Stashed changes

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

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _clienteRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
