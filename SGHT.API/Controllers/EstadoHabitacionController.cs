using Microsoft.AspNetCore.Mvc;
using SGHT.Application.Interfaces;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.API.Utils;
using Microsoft.AspNetCore.Authorization;

namespace SGHT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstadoHabitacionController : BaseController
    {
        private readonly IEstadoHabitacionService _estadoHabitacionService;

        public EstadoHabitacionController(IEstadoHabitacionService estadoHabitacionService)
        {
            _estadoHabitacionService = estadoHabitacionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _estadoHabitacionService.GetAll();
            return HandleResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _estadoHabitacionService.GetById(id);
            return HandleResponse(result);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Save(SaveEstadoHabitacionDto dto)
        {
            var result = await _estadoHabitacionService.Save(dto);
            return HandleResponse(result);
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> Update(UpdateEstadoHabitacionDto dto)
        {
            var result = await _estadoHabitacionService.UpdateById(dto);
            return HandleResponse(result);
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(DeleteEstadoHabitacionDto dto)
        {
            var result = await _estadoHabitacionService.DeleteById(dto);
            return HandleResponse(result);
        }
    }
}
