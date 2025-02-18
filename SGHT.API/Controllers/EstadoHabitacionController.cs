using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoHabitacionController : ControllerBase
    {
        private readonly IEstadoHabitacionRepository _estadoHabitacionRepository;

        public EstadoHabitacionController(IEstadoHabitacionRepository estadoHabitacionRepository, ILogger<EstadoHabitacionController> logger)
        {
            _estadoHabitacionRepository = estadoHabitacionRepository;
        }
        

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _estadoHabitacionRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
