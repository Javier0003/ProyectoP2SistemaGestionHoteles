<<<<<<< HEAD
﻿using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> main
using SGHT.API.Utils;
using SGHT.Application.Dtos.Categoria;
using SGHT.Application.Interfaces;
using SGHT.Domain.Entities;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : BaseController
    {
<<<<<<< HEAD
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService, ILogger<CategoriaController> logger)
=======
        private readonly ICategoriaService _categoriaRepository;

        public CategoriaController(ICategoriaService categoriaRepository, ILogger<CategoriaController> logger)
>>>>>>> main
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("GetCategoria")]
        public async Task<IActionResult> Get()
        {
<<<<<<< HEAD
            var result = await _categoriaService.GetAll();
            return HandleResponse(result);
=======
            var Usuarios = await _categoriaRepository.GetAll();
            return HandleResponse(Usuarios);
>>>>>>> main
        }

        [HttpGet("GetCategoriaId")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
<<<<<<< HEAD
            var result = await _categoriaService.GetById(id);
            return HandleResponse(result);
=======
            var categorias = await _categoriaRepository.GetById(id);

            if (categorias is null)
                return NotFound("Categoria no encontrada");
            
            return HandleResponse(categorias);
>>>>>>> main
        }

        [HttpPost("CreateCategoria")]
        public async Task<IActionResult> CrearCategoria(SaveCategoriaDto categoria)
<<<<<<< HEAD
        {   
            var result = await _categoriaService.Save(categoria);
=======
        {
            var result = await _categoriaRepository.Save(categoria);
>>>>>>> main
            return HandleResponse(result);
        }

        [HttpPatch("UpdateCategoria")]
        public async Task<IActionResult> ActualizarCategoria(UpdateCategoriaDto categoria)
         {
<<<<<<< HEAD
            var result = await _categoriaService.UpdateById(categoria);
=======
            var result = await _categoriaRepository.UpdateById(categoria);
>>>>>>> main
            return HandleResponse(result);
        }

        [HttpDelete("DeleteCategaria")]
<<<<<<< HEAD
        public async Task<IActionResult> EliminarCategoria(DeleteCategoriaDto categoria)
        {
            var result = await _categoriaService.DeleteById(categoria);
=======
        public async Task<IActionResult> EliminarCategoria(DeleteCategoriaDto id)
        {
            var result = await _categoriaRepository.DeleteById(id);
>>>>>>> main
            return HandleResponse(result);
        }
    }
}
