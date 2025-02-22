using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
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

        [HttpGet("GetHabitacionId")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var habitacion = await _habitacionRepository.GetEntityByIdAsync(id);

            if (habitacion is null)
                return NotFound("Habitacion no encontrada");

            return Ok(habitacion);
        }

        [HttpPost("CreateHabitacion")]
        public async Task<IActionResult> CrearHabitacion(Habitacion habitacion)
        {
            if (habitacion is null)
                return BadRequest("Esto no puede ser null");

            var result = await _habitacionRepository.SaveEntityAsync(habitacion);

            if (!result.Success)
                return Problem("Hubo un error al guardar la habitacion");

            return Ok();
        }

        [HttpPatch("UpdateHabitacion")]
        public async Task<IActionResult> ActualizarHabitacion(Habitacion habitacion)
        {
            if (habitacion is null)
                return BadRequest("Habitacion no puede ser nulo");

            var result = await _habitacionRepository.UpdateEntityAsync(habitacion);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("DeleteHabitacion")]
        public async Task<IActionResult> EliminarHabitacion(int id)
        {
            var habitacion = await _habitacionRepository.GetEntityByIdAsync(id);

            if (habitacion is null)
                return NotFound("Habitacion no encontrada");

            var result = await _habitacionRepository.DeleteEntityAsync(habitacion);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Habitacion eliminada");
        }
    }
}
