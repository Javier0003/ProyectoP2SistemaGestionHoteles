using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces.Configuration;
using SGHT.Persistance.Repositories;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private readonly IHabitacionRepository _habitacionRepository;

        public HabitacionController(IHabitacionRepository habitacionRepository, ILogger<HabitacionController> logger)
        {
            _habitacionRepository = habitacionRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _habitacionRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
