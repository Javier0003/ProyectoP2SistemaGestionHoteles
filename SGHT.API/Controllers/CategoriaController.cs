using Microsoft.AspNetCore.Mvc;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository, ILogger<CategoriaController> logger)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet("GetCategoria")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _categoriaRepository.GetAllAsync();
            return Ok(Usuarios);
        }

        [HttpGet("GetCategoriaId")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var categorias = await _categoriaRepository.GetEntityByIdAsync(id);

            if (categorias is null)
                return NotFound("Categoria no encontrada");
            
            return Ok(categorias);
        }

        [HttpPost("CreateCategoria")]
        public async Task<IActionResult> CrearCategoria(Categoria categoria)
        {
            if (categoria is null) 
                return BadRequest("Esto no puede ser nulo");
            
            var result = await _categoriaRepository.SaveEntityAsync(categoria);
            
            if (!result.Success) 
                return Problem("Hubo un error al guardar la categoría");

            return Ok();
        }

        [HttpPatch("UpdateCategoria")]
        public async Task<IActionResult> ActualizarCategoria(Categoria categoria)
         {
            if (categoria is null)
                return BadRequest("Categoria no puede ser nula");

            var result = await _categoriaRepository.UpdateEntityAsync(categoria);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("DeleteCategaria")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _categoriaRepository.GetEntityByIdAsync(id);

            if (categoria is null)
                return NotFound("Categoria no encontrada");

            var result = await _categoriaRepository.DeleteEntityAsync(categoria);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Categoria eliminada");
        }
    }
}
