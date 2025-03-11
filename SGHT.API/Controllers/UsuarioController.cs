using Microsoft.AspNetCore.Mvc;
using SGHT.Application.Interfaces;
using SGHT.Application.Dtos.Usuarios;
using SGHT.API.Utils;
using SGHT.Persistance.Entities.Users;
using Microsoft.AspNetCore.Authorization;

namespace SGHT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            var result = await _usuarioService.GetAll();
            return HandleResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _usuarioService.GetById(id);
            return HandleResponse(result);
        }

        [HttpPost("crear")]
        public async Task<IActionResult> Save(SaveUsuarioDto dto)
        {
            var result = await _usuarioService.Save(dto);
            return HandleResponse(result);
        }

        [HttpPatch("actualizar")]
        public async Task<IActionResult> Update(UpdateUsuarioDto dto)
        {
            var result = await _usuarioService.UpdateById(dto);
            return HandleResponse(result);
        }

        [HttpDelete("eliminar")]
        public async Task<IActionResult> Delete(DeleteUsuarioDto dto)
        {
            var result = await _usuarioService.DeleteById(dto);

            return HandleResponse(result);
        }

        [HttpPost("logIn")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(UserLogIn usuario)
        {
            var result = await _usuarioService.LogIn(usuario);
            return HandleResponse(result);
        }
    }
}