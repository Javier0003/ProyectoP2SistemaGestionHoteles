using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuarioController(IUsuariosRepository usuariosRepository, ILogger<UsuarioController> logger) {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _usuariosRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
