using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces.Reservation;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifasController : ControllerBase
    {
        private readonly ITarifasRepository _tarifasRepository;

        public TarifasController(ITarifasRepository tarifasRepository, ILogger<UsuarioController> logger)
        {
            _tarifasRepository = tarifasRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _tarifasRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
