using Microsoft.AspNetCore.Mvc;
using SGHT.Persistance.Interfaces.Reservation;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServiciosRepository _serviciosRepository;

        public ServiciosController(IServiciosRepository serviciosRepository, ILogger<ServiciosController> logger)
        {
            _serviciosRepository = serviciosRepository;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _serviciosRepository.GetAllAsync();
            return Ok(Usuarios);
        }
    }
}
