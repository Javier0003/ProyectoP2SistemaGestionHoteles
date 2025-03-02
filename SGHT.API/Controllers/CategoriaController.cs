using Microsoft.AspNetCore.Mvc;
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
        private readonly ICategoriaService _categoriaRepository;

        public CategoriaController(ICategoriaService categoriaRepository, ILogger<CategoriaController> logger)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet("GetCategoria")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _categoriaRepository.GetAll();
            return HandleResponse(Usuarios);
        }

        [HttpGet("GetCategoriaId")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var categorias = await _categoriaRepository.GetById(id);

            if (categorias is null)
                return NotFound("Categoria no encontrada");
            
            return HandleResponse(categorias);
        }

        [HttpPost("CreateCategoria")]
        public async Task<IActionResult> CrearCategoria(SaveCategoriaDto categoria)
        {
            var result = await _categoriaRepository.Save(categoria);
            return HandleResponse(result);
        }

        [HttpPatch("UpdateCategoria")]
        public async Task<IActionResult> ActualizarCategoria(UpdateCategoriaDto categoria)
         {
            var result = await _categoriaRepository.UpdateById(categoria);
            return HandleResponse(result);
        }

        [HttpDelete("DeleteCategaria")]
        public async Task<IActionResult> EliminarCategoria(DeleteCategoriaDto id)
        {
            var result = await _categoriaRepository.DeleteById(id);
            return HandleResponse(result);
        }
    }
}
