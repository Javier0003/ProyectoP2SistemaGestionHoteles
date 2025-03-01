using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Application.Base;
using SGHT.Application.Dtos.Categoria;
using SGHT.Application.Interfaces;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Interfaces;
using SGHT.Persistance.Repositories;

namespace SGHT.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriaService> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaService(ICategoriaRepository categoriaRepository, ILogger<CategoriaService> logger, IConfiguration configuration)
        {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<OperationResult> GetAll()
        {
            try 
            {
                var categorias = await _categoriaRepository.GetAllAsync();
                return OperationResult.GetSuccesResult(categorias, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk there's a error here", code: 500);
            } 
        }

        public async Task<OperationResult> GetById(int id)
        {
            try
            {
                var categorias = await _categoriaRepository.GetEntityByIdAsync(id);

                if (categorias is null)
                   return OperationResult.GetErrorResult("Categoria no encontrada", code: 404);

                return OperationResult.GetSuccesResult(categorias, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaService.GetALl: {ex.ToString()}");
                return OperationResult.GetErrorResult("idk there's a error here", code: 500);
            }
        }

        public async Task<OperationResult> Save(SaveCategoriaDto dto)
        {
            Categoria categoria = new()
            {
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                FechaCreacion = DateTime.Now
            };

            try
            {
                var categorias = await _categoriaRepository.SaveEntityAsync(categoria);
                if (!categorias.Success) throw new Exception();

                return OperationResult.GetSuccesResult(categorias, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaService.Save: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurriod un error creando una nueva categoria", code: 500);
            }
        }

        public async Task<OperationResult> UpdateById(UpdateCategoriaDto dto)
        {
            Categoria categoria = new()
            {
                IdCategoria = dto.IdCategoria,
                Descripcion = dto.Descripcion,
                Estado = dto.Estado,
                FechaCreacion = DateTime.Now
            };

            try
            {
                var queryResult = await _categoriaRepository.UpdateEntityAsync(categoria);
                if (!queryResult.Success) throw new Exception();

                return OperationResult.GetSuccesResult(queryResult, code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaService.UpdateById: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurrido un error al actualizar las categorias", code: 500);
            }
        }

        public async Task<OperationResult> DeleteById(DeleteCategoriaDto dto)
        {
            try
            {
                var categoria = await _categoriaRepository.GetEntityByIdAsync(dto.IdCategoria);
               
                if (categoria is null) 
                     return OperationResult.GetErrorResult("La categoria con este id no existe", code: 404);

                var queryResult = await _categoriaRepository.DeleteEntityAsync(categoria);
                
                if (!queryResult.Success) 
                    return OperationResult.GetErrorResult("Ha ocurrido un error al eliminar esta categoria", code: 500);

                return OperationResult.GetSuccesResult(queryResult, "Categoria eliminada correctamente", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaService.DeletebyId: {ex.ToString()}");
                return OperationResult.GetErrorResult("Ha ocurrido un rrror eliminando categoria", code: 500);
            }
        }
    }
}
