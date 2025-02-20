using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecepcionController : ControllerBase
    {
        private readonly IRecepcionRepository _recepcionRepository;

        public RecepcionController(IRecepcionRepository recepcionRepository, ILogger<RecepcionController> logger)
        {
            _recepcionRepository = recepcionRepository;
        }

        [HttpGet("GetRecepcion")]
        public async Task<IActionResult> Get()
        {
            var result = await _recepcionRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetRecepcionByIDCliente")]
        public async Task<IActionResult> GetUsuariosByID(int IDcliente)
        {
            var result = await _recepcionRepository.GetRecepcionByClienteID(IDcliente);
            if(result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("CrearRecepcion")]
        public async Task<IActionResult> Post(Recepcion recepcion)
        {
            if (recepcion == null) return BadRequest("Peticion invalida");

            var result = await _recepcionRepository.SaveEntityAsync(recepcion);

            if (!result.Success) return BadRequest();
            return Ok("Recepcion Creada");
        }

        [HttpDelete("EliminarRecepcion/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest("Id is null");
            bool isParsed = int.TryParse(id, out int result);
            if (!isParsed) return BadRequest("Id must be a String");

            var entity = await _recepcionRepository.GetEntityByIdAsync(result);
            if (entity == null) return NotFound("Recepcion no encontrada");

            var queryResult = await _recepcionRepository.DeleteEntityAsync(entity);
            if (!queryResult.Success) return Problem();

            return Ok("Recepcion Eliminada");
        }

        [HttpPatch("UpdateRecepcion")]
        public async Task<IActionResult> Put([FromBody] Recepcion recepcion)
        {
            if (recepcion == null)
                return BadRequest("Recepcion no puede ser nulo");

            var result = await _recepcionRepository.UpdateEntityAsync(recepcion);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
