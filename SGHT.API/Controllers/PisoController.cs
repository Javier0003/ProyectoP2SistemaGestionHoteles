using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisoController : ControllerBase
    {
        private readonly IPisoRepository _pisoRepository;

        public PisoController(IPisoRepository pisoRepository, ILogger<PisoController> logger)
        {
            _pisoRepository = pisoRepository;
        }

        [HttpGet("GetPiso")]
        public async Task<IActionResult> Get()
        {
            var Piso = await _pisoRepository.GetAllAsync();
            return Ok(Piso);
        }

         [HttpGet("GetPisoById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            bool isParsed = int.TryParse(id, out int result);
            if (!isParsed) return BadRequest("Id must be a number");

            var Piso = await _pisoRepository.GetEntityByIdAsync(result);
            if (Piso == null) return NotFound("Role not found");

            return Ok(Piso);
        }

        [HttpPost("CrearPiso")]
        public async Task<IActionResult> Post(Piso piso)
        {
            if (piso == null) return BadRequest("Peticion invalida");

            var result = await _pisoRepository.SaveEntityAsync(piso);

            if (!result.Success) return BadRequest();
            return Ok("Piso Asignado");
        }


        [HttpDelete("EliminarPiso/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest("Id is null");
            bool isParsed = int.TryParse(id, out int result);
            if (!isParsed) return BadRequest("Id must be a String");

            var entity = await _pisoRepository.GetEntityByIdAsync(result);
            if (entity == null) return NotFound("Piso no encontrado");

            var queryResult = await _pisoRepository.DeleteEntityAsync(entity);
            if (!queryResult.Success) return Problem();

            return Ok("piso eliminado");
        }

        [HttpPatch("ActualizarPiso")]
        public async Task<IActionResult> Put(Piso piso)
        {
            if (piso == null) return BadRequest("body is null");
            var queryResult = await _pisoRepository.UpdateEntityAsync(piso);
            if (!queryResult.Success) return Problem();

            return Ok();
        }
    }
}
