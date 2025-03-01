using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.Application.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoHabitacionController : BaseController
    {
        private readonly IEstadoHabitacionService _estadoHabitacionService;

        public EstadoHabitacionController(IEstadoHabitacionService estadoHabitacionService, ILogger<EstadoHabitacionController> logger)
        {
            _estadoHabitacionService = estadoHabitacionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var estadoHabitacionList = await _estadoHabitacionService.GetAll();
            return HandleResponse(estadoHabitacionList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var estadoHabitacion = await _estadoHabitacionService.GetById(id);
            if (!estadoHabitacion.Success) return NotFound("estate not found");

            return HandleResponse(estadoHabitacion);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Post(SaveEstadoHabitacionDto estadoHabitacion)
        {
            var result = await _estadoHabitacionService.Save(estadoHabitacion);

            return HandleResponse(result);
        }


        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(DeleteEstadoHabitacionDto id)
        {
            var result = await _estadoHabitacionService.DeleteById(id);

            return HandleResponse(result);
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> Put(UpdateEstadoHabitacionDto estadoHabitacion)
        {
            var result = await _estadoHabitacionService.UpdateById(estadoHabitacion);
            return HandleResponse(result);
        }
    }
}