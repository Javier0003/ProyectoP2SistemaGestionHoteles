using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
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
        

      [HttpGet("GetEstadoHabitacion")]
        public async Task<IActionResult> Get()
        {
            var Estado = await _estadoHabitacionRepository.GetAllAsync();
            return Ok(Estado);
        }

        [HttpGet("GetEstadoHabitacionById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            bool isParsed = int.TryParse(id, out int result);
            if (!isParsed) return BadRequest("Id must be a number");

            var Estado = await _estadoHabitacionRepository.GetEntityByIdAsync(result);
            if (Estado == null) return NotFound("Role not found");

            return Ok(Estado);
        }

         [HttpPatch("ActualizarEstadoHabitacion")]
        public async Task<IActionResult> Put(EstadoHabitacion estadoHabitacion)
        {
            if (estadoHabitacion == null) return BadRequest("body is null");
            var queryResult = await _estadoHabitacionRepository.UpdateEntityAsync(estadoHabitacion);
            if (!queryResult.Success) return Problem();

            return Ok();
        }

    

    }
}
