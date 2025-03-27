using Microsoft.AspNetCore.Mvc;
using SGHT.Application.Interfaces;
using SGHT.Application.Dtos.Usuarios;
using SGHT.API.Utils;
using SGHT.Persistance.Entities.Users;
using Microsoft.AspNetCore.Authorization;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;

namespace SGHT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _usuarioService.GetAll();
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!IsIdValid(id)) return BadRequest("Invalid id");
            try
            {
                var result = await _usuarioService.GetById(id);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPost("crear")]
        [AllowAnonymous]
        public async Task<IActionResult> Save(SaveUsuarioDto dto)
        {
            if (!IsIdValid(dto.IdRolUsuario)) return BadRequest("Invalid roleId");
            try
            {
                var result = await _usuarioService.Save(dto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> Update(UpdateUsuarioDto dto)
        {
            if (!IsIdValid(dto.IdRolUsuario)) return BadRequest("Invalid roleId");
            if (!IsIdValid(dto.IdUsuario)) return BadRequest("Invalid id");
            try
            {
                var result = await _usuarioService.UpdateById(dto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(DeleteUsuarioDto dto)
        {
            if (!IsIdValid(dto.IdUsuario)) return BadRequest("Invalid id");
            try
            {
                var result = await _usuarioService.DeleteById(dto);
                return HandleResponse(result);
            }
            catch (Exception ex)
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }
        }

        [HttpPost("logIn")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(UserLogIn usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Email)) return BadRequest("password can't be null");
            if (string.IsNullOrWhiteSpace(usuario.Password)) return BadRequest("password can't be null");
            try
            {
                var result = await _usuarioService.LogIn(usuario);
                return HandleResponse(result);
            }
            catch (Exception ex) 
            {
                return HandleResponse(OperationResult.GetErrorResult("Internal Server Error", code: 500));
            }

        }
    }
}