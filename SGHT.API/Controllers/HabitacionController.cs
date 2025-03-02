using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
<<<<<<< HEAD
using SGHT.Application.Dtos.Categoria;
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Interfaces;
using SGHT.Application.Services;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
=======
using SGHT.Application.Dtos.Habitacion;
using SGHT.Application.Interfaces;
>>>>>>> main

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : BaseController
    {
<<<<<<< HEAD
        private readonly IHabitacionService _habitacionService;

        public HabitacionController(IHabitacionService habitacionService, ILogger<HabitacionController> logger)
=======
        private readonly IHabitacionService _habitacionRepository;

        public HabitacionController(IHabitacionService habitacionRepository, ILogger<HabitacionController> logger)
>>>>>>> main
        {
            _habitacionService = habitacionService;
        }

        [HttpGet("GetHabitaciones")]
        public async Task<IActionResult> Get()
        {
<<<<<<< HEAD
            var result = await _habitacionService.GetAll();
            return HandleResponse(result);
=======
            var Usuarios = await _habitacionRepository.GetAll();
            return HandleResponse(Usuarios);
>>>>>>> main
        }

        [HttpGet("GetHabitacionId/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
<<<<<<< HEAD
            var result = await _habitacionService.GetById(id);
            return HandleResponse(result);
=======
            var habitacion = await _habitacionRepository.GetById(id);
            return HandleResponse(habitacion);
>>>>>>> main
        }

        [HttpPost("CreateHabitacion")]
        public async Task<IActionResult> CrearHabitacion(SaveHabitacionDto habitacion)
        {
<<<<<<< HEAD
            var result = await _habitacionService.Save(habitacion);
=======
            var result = await _habitacionRepository.Save(habitacion);
>>>>>>> main
            return HandleResponse(result);
        }

        [HttpPatch("UpdateHabitacion")]
        public async Task<IActionResult> ActualizarHabitacion(UpdateHabitacionDto habitacion)
        {
<<<<<<< HEAD
            var result = await _habitacionService.UpdateById(habitacion);
            return HandleResponse(result);
=======
           var result = await _habitacionRepository.UpdateById(habitacion);
           return HandleResponse(result);
>>>>>>> main
        }

        [HttpDelete("DeleteHabitacion")]
<<<<<<< HEAD
        public async Task<IActionResult> EliminarHabitacion(DeleteHabitacionDto habitacion)
        {
            var result = await _habitacionService.DeleteById(habitacion);
=======
        public async Task<IActionResult> EliminarHabitacion(DeleteHabitacionDto id)
        {
            var result = await _habitacionRepository.DeleteById(id);
>>>>>>> main
            return HandleResponse(result);
        }
    }
}
