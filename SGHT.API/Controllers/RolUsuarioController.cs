using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.RolUsuario;
using SGHT.Application.Dtos.Usuarios;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;

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
            try
            {
                var rolUsuarioList = await _rolUsuarioService.GetAll();
                return HandleResponse(rolUsuarioList);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!IsIdValid(id)) return BadRequest("invalid id");
            try
            {
                var rolUsuario = await _rolUsuarioService.GetById(id);
                if (!rolUsuario.Success) return NotFound("user not found");

                return HandleResponse(rolUsuario);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Post(SaveRolUsuarioDto rolUsuario)
        {
            if (string.IsNullOrWhiteSpace(rolUsuario.Descripcion)) return BadRequest("descripcion can't be null");
            try
            {
                var result = await _rolUsuarioService.Save(rolUsuario);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }


        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(DeleteRolUsuarioDto id)
        {
            if(!IsIdValid(id.IdRolUsuario)) return BadRequest("id is not valid");
            try
            {
                var result = await _rolUsuarioService.DeleteById(id);

                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> Put(UpdateRolUsuarioDto rolUsuario)
        {
            if (!IsIdValid(rolUsuario.IdRolUsuario)) return BadRequest("id is not valid");
            if (string.IsNullOrWhiteSpace(rolUsuario.Descripcion)) return BadRequest("descripcion can't be null");
            try
            {
                var result = await _rolUsuarioService.UpdateById(rolUsuario);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }
    }
}
