using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.EstadoHabitacion;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;

namespace SGHT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoHabitacionController : BaseController
    {
        private readonly IEstadoHabitacionService _estadoHabitacionService;

        public EstadoHabitacionController(IEstadoHabitacionService estadoHabitacionService, ILogger<EstadoHabitacionController> logger)
        {
            _estadoHabitacionService = estadoHabitacionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstadosHabitacion()
        {
            try
            {
                var estados = await _estadoHabitacionService.GetAll();
                return HandleResponse(estados);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstadoHabitacionById(int id)
        {
            if (!IsIdValid(id)) return BadRequest("Invalid id");
            try
            {
                var estado = await _estadoHabitacionService.GetById(id);
                return HandleResponse(estado);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> CreateEstadoHabitacion(SaveEstadoHabitacionDto estadoHabitacion)
        {
            try
            {
                var result = await _estadoHabitacionService.Save(estadoHabitacion);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> EliminarEstadoHabitacion(DeleteEstadoHabitacionDto dto)
        {
            if (!IsIdValid(dto.IdEstadoHabitacion)) return BadRequest("Invalid id");
            try
            {
                var result = await _estadoHabitacionService.DeleteById(dto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> ActualizarEstadoHabitacion(UpdateEstadoHabitacionDto estadoHabitacion)
        {
            if (!IsIdValid(estadoHabitacion.IdEstadoHabitacion)) return BadRequest("Invalid id");
            try
            {
                var result = await _estadoHabitacionService.UpdateById(estadoHabitacion);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }
    }
}
