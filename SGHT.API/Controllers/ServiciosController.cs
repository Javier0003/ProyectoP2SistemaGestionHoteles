
﻿using Microsoft.AspNetCore.Http;
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
        private readonly IServiciosService _serviciosService;

        public ServiciosController(IServiciosService serviciosService, ILogger<ServiciosController> logger)
        {
            _serviciosService = serviciosService;
        }

        [HttpGet("GetServicios")]
        public async Task<IActionResult> Get()
        {
            var result = await _serviciosService.GetAll();
            return HandleResponse(result);
        }

        [HttpGet("GetServicioId/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var result = await _serviciosService.GetById(id);
            return HandleResponse(result);
        }

        [HttpPost("CreateServicio")]
        public async Task<IActionResult> CrearServicio(SaveServiciosDto servicio)
        {
            var result = await _serviciosService.Save(servicio);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateServicios")] 
        public async Task<IActionResult> ActualizarServicio(UpdateServiciosDto servicio)
        {
            var result = await _serviciosService.UpdateById(servicio);
            return HandleResponse(result);
        }

        [HttpDelete("DeleteServicios")]
        public async Task<IActionResult> EliminarCategoria(DeleteServiciosDto servicio)
        {
            var result = await _serviciosService.DeleteById(servicio);
            return HandleResponse(result);
        }
    }
}
