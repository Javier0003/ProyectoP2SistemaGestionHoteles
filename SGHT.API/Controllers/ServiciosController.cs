<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Servicio;
using SGHT.Application.Interfaces;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;
=======
﻿using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Servicio;
using SGHT.Application.Interfaces;
>>>>>>> main

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : BaseController
    {
<<<<<<< HEAD
        private readonly IServiciosService _serviciosService;

        public ServiciosController(IServiciosService serviciosService, ILogger<ServiciosController> logger)
=======
        private readonly IServiciosService _serviciosRepository;

        public ServiciosController(IServiciosService serviciosRepository, ILogger<ServiciosController> logger)
>>>>>>> main
        {
            _serviciosService = serviciosService;
        }

        [HttpGet("GetServicios")]
        public async Task<IActionResult> Get()
        {
<<<<<<< HEAD
            var result = await _serviciosService.GetAll();
            return HandleResponse(result);
=======
            var Usuarios = await _serviciosRepository.GetAll();
            return HandleResponse(Usuarios);
>>>>>>> main
        }

        [HttpGet("GetServicioId/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
<<<<<<< HEAD
            var result = await _serviciosService.GetById(id);
            return HandleResponse(result);
=======
            var categorias = await _serviciosRepository.GetById(id);
            return HandleResponse(categorias);
>>>>>>> main
        }

        [HttpPost("CreateServicio")]
        public async Task<IActionResult> CrearServicio(SaveServiciosDto servicio)
        {
<<<<<<< HEAD
            var result = await _serviciosService.Save(servicio);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateServicios")] 
        public async Task<IActionResult> ActualizarServicio(UpdateServiciosDto servicio)
        {
            var result = await _serviciosService.UpdateById(servicio);
=======
            var result = await _serviciosRepository.Save(servicio);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateServicios")]
        public async Task<IActionResult> ActualizarServicio(UpdateServiciosDto servicio)
        {
            var result = await _serviciosRepository.UpdateById(servicio);
>>>>>>> main
            return HandleResponse(result);
        }

        [HttpDelete("DeleteServicios")]
<<<<<<< HEAD
        public async Task<IActionResult> EliminarCategoria(DeleteServiciosDto servicio)
        {
            var result = await _serviciosService.DeleteById(servicio);
=======
        public async Task<IActionResult> EliminarCategoria(DeleteServiciosDto id)
        {
            var result = await _serviciosRepository.DeleteById(id);
>>>>>>> main
            return HandleResponse(result);
        }
    }
}
