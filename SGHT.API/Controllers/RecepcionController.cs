using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecepcionController : ControllerBase
    {
        private readonly IRecepcionRepository _recepcionRepository;

        public RecepcionController(IRecepcionRepository recepcionRepository, ILogger<RecepcionController> logger)
        {
            _recepcionRepository = recepcionRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _recepcionRepository.GetAllAsync();
            return Ok(Usuarios);
        }

        [HttpGet("GetUsuariosByID")]
        public async Task<IActionResult> GetUsuariosByID(int id)
        {
            var Usuarios = await _recepcionRepository.GetEntityByIdAsync(id);
            return Ok(Usuarios);
        }
    }
}
