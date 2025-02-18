using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : ControllerBase
    {
        private readonly IRolUsuarioRepository _rolUsuarioRepository;

        public RolUsuarioController(IRolUsuarioRepository rolUsuarioRepository, ILogger<RolUsuarioController> logger)
        {
            _rolUsuarioRepository = rolUsuarioRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _rolUsuarioRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
