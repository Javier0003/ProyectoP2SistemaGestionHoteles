using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisoController : ControllerBase
    {
        private readonly IPisoRepository _pisoRepository;

        public PisoController(IPisoRepository pisoRepository, ILogger<PisoController> logger)
        {
            _pisoRepository = pisoRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _pisoRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
