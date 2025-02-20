using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuarioController(IUsuariosRepository usuariosRepository, ILogger<UsuarioController> logger) {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet("getUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuariosRepository.GetAllAsync();

            return Ok(usuarios);
        }

        [HttpGet("getUsuarioById/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            bool isParsed =  int.TryParse(id, out int result);
            if (!isParsed) return BadRequest("Id must be a number");

            var Usuarios = await _usuariosRepository.GetEntityByIdAsync(result);
            if (Usuarios == null) return NotFound("User with that Id was not found");

            return Ok(Usuarios);
        }

        [HttpPost("CrearUsuario")]
        public async Task<IActionResult> Post(Usuarios usuarios)
        {
            if(usuarios == null) return BadRequest("Input can't be null");

            var result = await _usuariosRepository.SaveEntityAsync(usuarios);
            if (!result.Success) return Problem();

            return Ok();
        }

        [HttpDelete("EliminarUsuario/{id}")]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            if (id == null) return BadRequest();
            bool isParsed = int.TryParse(id, out int parseResult);
            if (!isParsed) return BadRequest("Id must be a number");

            var entityToRemove = await _usuariosRepository.GetEntityByIdAsync(parseResult);
            if (entityToRemove == null) return NotFound("Usuario no encontrado");

            var result = await _usuariosRepository.DeleteEntityAsync(entityToRemove);
            if(!result.Success) return Problem();

            return Ok();
        }

        [HttpPatch("ActualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario(Usuarios usuarios)
        {
            if (usuarios == null) return BadRequest();

            var queryResult = await _usuariosRepository.UpdateEntityAsync(usuarios);
            if (!queryResult.Success) return Problem();

            return Ok();
        }
    }
}