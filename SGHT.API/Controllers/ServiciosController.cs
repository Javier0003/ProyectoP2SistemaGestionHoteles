using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Servicio;
using SGHT.Application.Interfaces;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : BaseController
    {
        private readonly IServiciosService _serviciosRepository;

        public ServiciosController(IServiciosService serviciosRepository, ILogger<ServiciosController> logger)
        {
            _serviciosRepository = serviciosRepository;
        }

        [HttpGet("GetServicios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _serviciosRepository.GetAll();
            return HandleResponse(Usuarios);
        }

        [HttpGet("GetServicioId/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var categorias = await _serviciosRepository.GetById(id);
            return HandleResponse(categorias);
        }

        [HttpPost("CreateServicio")]
        public async Task<IActionResult> CrearServicio(SaveServiciosDto servicio)
        {
            var result = await _serviciosRepository.Save(servicio);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateServicios")]
        public async Task<IActionResult> ActualizarServicio(UpdateServiciosDto servicio)
        {
            var result = await _serviciosRepository.UpdateById(servicio);
            return HandleResponse(result);
        }

        [HttpDelete("DeleteServicios")]
        public async Task<IActionResult> EliminarCategoria(DeleteServiciosDto id)
        {
            var result = await _serviciosRepository.DeleteById(id);
            return HandleResponse(result);
        }
    }
}
