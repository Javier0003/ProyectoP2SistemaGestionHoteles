using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolUsuarioController : BaseController
    {
        private readonly IRolUsuarioService _rolUsuarioService;

        public RolUsuarioController(IRolUsuarioService rolUsuarioService, ILogger<RolUsuarioController> logger)
        {
            _rolUsuarioService = rolUsuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rolUsuarioList = await _rolUsuarioService.GetAll();
            return HandleResponse(rolUsuarioList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rolUsuario = await _rolUsuarioService.GetById(id);
            if (!rolUsuario.Success) return NotFound("user not found");

            return HandleResponse(rolUsuario);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Post(SaveRolUsuarioDto rolUsuario)
        {
            var result = await _rolUsuarioService.Save(rolUsuario);

            return HandleResponse(result);
        }


        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(DeleteRolUsuarioDto id)
        {
            var result = await _rolUsuarioService.DeleteById(id);

            return HandleResponse(result);
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> Put(UpdateRolUsuarioDto rolUsuario)
        {
            var result = await _rolUsuarioService.UpdateById(rolUsuario);
            return HandleResponse(result);
        }
    }
}
