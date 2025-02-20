using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifasController : ControllerBase
    {
        private readonly ITarifasRepository _tarifasRepository;

        public TarifasController(ITarifasRepository tarifasRepository, ILogger<UsuarioController> logger)
        {
            _tarifasRepository = tarifasRepository;
        }

        [HttpGet("GetTarifas")]
        public async Task<IActionResult> GetTarifas()
        {
            var Usuarios = await _tarifasRepository.GetAllAsync();
            return Ok(Usuarios);
        }

        [HttpGet("GetTarifas/{id}")]
        public async Task<IActionResult> GetTarifaById(string id)
        {
            if (id == null) return BadRequest();
            bool isParsed = int.TryParse(id, out var parsedId);
            if(!isParsed) return BadRequest("Id must be a number");

            var result = await _tarifasRepository.GetEntityByIdAsync(parsedId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("CreateTarifa")]
        public async Task<IActionResult> CreateTarifa(Tarifas tarifas)
        {
            if (tarifas == null) return BadRequest(new {Success = false, Msg = "Body is null"});
            if (tarifas.Estado != "inactivo" && tarifas.Estado != "activo") return BadRequest(new {Success = false, Msg = "estado tiene que ser 'activo' o 'inactivo'" });

            var result = await _tarifasRepository.SaveEntityAsync(tarifas);

            return Ok(new
            {
                Success = true,
                Tarifa = result
            });
        }

        [HttpDelete("EliminarTarifa/{id}")]
        public async Task<IActionResult> EliminarTarifa(string id)
        {
            if (id == null) return BadRequest();
            bool isParsed = int.TryParse(id, out int parseResult);
            if (!isParsed) return BadRequest("Id must be a number");

            var entityToRemove = await _tarifasRepository.GetEntityByIdAsync(parseResult);
            if (entityToRemove == null) return NotFound("Tarifa no encontrada");

            var result = await _tarifasRepository.DeleteEntityAsync(entityToRemove);
            if (!result.Success) return Problem();

            return Ok(new {Success = true});
        }

        [HttpPatch("ActualizarUsuario")]
        public async Task<IActionResult> ActualizarTarifa(Tarifas tarifas)
        {
            if (tarifas == null) return BadRequest();
            if (tarifas.Estado != "inactivo" && tarifas.Estado != "activo") return BadRequest(new {Success = false, Msg = "estado tiene que ser 'activo' o 'inactivo'" });

            var queryResult = await _tarifasRepository.UpdateEntityAsync(tarifas);
            if (!queryResult.Success) return Problem();

            return Ok(queryResult);
        }
    }
}
