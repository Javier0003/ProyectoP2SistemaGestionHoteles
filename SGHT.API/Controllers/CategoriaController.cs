using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using SGHT.API.Utils;
using SGHT.Application.Dtos.Categoria;
using SGHT.Application.Interfaces;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : BaseController
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService, ILogger<CategoriaController> logger)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("GetCategoria")]
        public async Task<IActionResult> Get()
        {
            var result = await _categoriaService.GetAll();
            return HandleResponse(result);
        }

        [HttpGet("GetCategoriaId")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _categoriaService.GetById(id);
            return HandleResponse(result);
        }

        [HttpPost("CreateCategoria")]
        public async Task<IActionResult> CrearCategoria(SaveCategoriaDto categoria)
        {   
            var result = await _categoriaService.Save(categoria);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateCategoria")]
        public async Task<IActionResult> ActualizarCategoria(UpdateCategoriaDto categoria)
         {
            var result = await _categoriaService.UpdateById(categoria);
            return HandleResponse(result);
        }

        [HttpDelete("DeleteCategaria")]
        public async Task<IActionResult> EliminarCategoria(DeleteCategoriaDto categoria)
        {
            var result = await _categoriaService.DeleteById(categoria);
            return HandleResponse(result);
        }
    }
}
