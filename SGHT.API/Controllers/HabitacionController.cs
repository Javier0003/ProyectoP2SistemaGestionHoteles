using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Categoria;
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : BaseController
    {
        private readonly IHabitacionService _habitacionService;

        public HabitacionController(IHabitacionService habitacionService, ILogger<HabitacionController> logger)
        {
            _habitacionService = habitacionService;
        }

        [HttpGet("GetHabitaciones")]
        public async Task<IActionResult> Get()
        {
            var result = await _habitacionService.GetAll();
            return HandleResponse(result);
        }

        [HttpGet("GetHabitacionId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _habitacionService.GetById(id);
            return HandleResponse(result);
        }

        [HttpPost("CreateHabitacion")]
        public async Task<IActionResult> CrearHabitacion(SaveHabitacionDto habitacion)
        {
            var result = await _habitacionService.Save(habitacion);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateHabitacion")]
        public async Task<IActionResult> ActualizarHabitacion(UpdateHabitacionDto habitacion)
        {
            var result = await _habitacionService.UpdateById(habitacion);
            return HandleResponse(result);
        }

        [HttpDelete("DeleteHabitacion")]
        public async Task<IActionResult> EliminarHabitacion(DeleteHabitacionDto habitacion)
        {
            var result = await _habitacionService.DeleteById(habitacion);
            return HandleResponse(result);
        }
    }
}
