using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : BaseController
    {
        private readonly IHabitacionService _habitacionRepository;

        public HabitacionController(IHabitacionService habitacionRepository, ILogger<HabitacionController> logger)
        {
            _habitacionRepository = habitacionRepository;
        }

        [HttpGet("GetHabitaciones")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _habitacionRepository.GetAll();
            return HandleResponse(Usuarios);
        }

        [HttpGet("GetHabitacionId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var habitacion = await _habitacionRepository.GetById(id);
            return HandleResponse(habitacion);
        }

        [HttpPost("CreateHabitacion")]
        public async Task<IActionResult> CrearHabitacion(SaveHabitacionDto habitacion)
        {
            var result = await _habitacionRepository.Save(habitacion);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateHabitacion")]
        public async Task<IActionResult> ActualizarHabitacion(UpdateHabitacionDto habitacion)
        {
           var result = await _habitacionRepository.UpdateById(habitacion);
           return HandleResponse(result);
        }
       
        [HttpDelete("DeleteHabitacion")]
        public async Task<IActionResult> EliminarHabitacion(DeleteHabitacionDto id)
        {
            var result = await _habitacionRepository.DeleteById(id);
            return HandleResponse(result);
        }
    }
}
