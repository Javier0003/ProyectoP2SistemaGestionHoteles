using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

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

        [HttpGet("GetServicioId")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var categorias = await _serviciosRepository.GetEntityByIdAsync(id);

            if (categorias is null)
                return NotFound("Servicio no encontrado");

            return Ok(categorias);
        }

        [HttpPost("CreateServicio")]
        public async Task<IActionResult> CrearServicio(Servicios servicio)
        {
            if (servicio is null)
                return BadRequest("Esto no puede ser null");

            var result = await _serviciosRepository.SaveEntityAsync(servicio);

            if (!result.Success)
                return Problem("Hubo un error al guardar el servicio");

            return Ok("Servicio creado exitosamente");
        }

        [HttpPatch("UpdateServicios")]
        public async Task<IActionResult> ActualizarServicio(Servicios servicio)
        {
            if (servicio is null)
                return BadRequest("El servicio no puede ser nulo");

            var result = await _serviciosRepository.UpdateEntityAsync(servicio);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("DeleteServicios")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var servicio = await _serviciosRepository.GetEntityByIdAsync(id);

            if (servicio is null)
                return NotFound("Servicio no encontrada");

            var result = await _serviciosRepository.DeleteEntityAsync(servicio);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Servicio eliminada");
        }
    }
}
