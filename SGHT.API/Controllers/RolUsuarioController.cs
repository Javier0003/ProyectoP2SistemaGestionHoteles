using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : ControllerBase
    {
        private readonly IRolUsuarioRepository _rolUsuarioRepository;

        public RolUsuarioController(IRolUsuarioRepository rolUsuarioRepository, ILogger<RolUsuarioController> logger)
        {
            _rolUsuarioRepository = rolUsuarioRepository;
        }

        [HttpGet("GetRolUsuario")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _rolUsuarioRepository.GetAllAsync();
            return Ok(Usuarios);
        }

        [HttpGet("GetRolUsuarioById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            bool isParsed = int.TryParse(id, out int result);
            if (!isParsed) return BadRequest("Id must be a number");

            var Usuarios = await _rolUsuarioRepository.GetEntityByIdAsync(result);
            if (Usuarios == null) return NotFound("Role not found");

            return Ok(Usuarios);
        }

        [HttpPost("CrearRolUsuario")]
        public async Task<IActionResult> Post(RolUsuario rolUsuario)
        {
            if (rolUsuario == null) return BadRequest("Peticion invalida");

            var result = await _rolUsuarioRepository.SaveEntityAsync(rolUsuario);

            if (!result.Success) return BadRequest();
            return Ok("Rol Creado");
        }


        [HttpDelete("EliminarRolUsuario/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return BadRequest("Id is null");
            bool isParsed = int.TryParse(id, out int result);
            if (!isParsed) return BadRequest("Id must be a String");

            var entity = await _rolUsuarioRepository.GetEntityByIdAsync(result);
            if (entity == null) return NotFound("Rol no encontrado");

            var queryResult = await _rolUsuarioRepository.DeleteEntityAsync(entity);
            if (!queryResult.Success) return Problem();

            return Ok("Rol Eliminado");
        }

        [HttpPatch("ActualizarRolUsuario")]
        public async Task<IActionResult> Put(RolUsuario rolUsuario)
        {
            if (rolUsuario == null) return BadRequest("body is null");
            var queryResult = await _rolUsuarioRepository.UpdateEntityAsync(rolUsuario);
            if (!queryResult.Success) return Problem();

            return Ok();
        }
    }
}
