using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Persistance.Base;
using SGHT.Persistance.Context;
using SGHT.Persistance.Interfaces;

namespace SGHT.Persistance.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        private readonly SGHTContext _context;
        private readonly ILogger<CategoriaRepository> _logger;
        private readonly IConfiguration _configuration;

        public CategoriaRepository(SGHTContext context, ILogger<CategoriaRepository> logger, IConfiguration configuration) : base(context)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }
 
        public override async Task<Categoria> GetEntityByIdAsync(int id)
        {
            try
            {
                var result = await base.GetEntityByIdAsync(id);
                if (result.Estado == false) return null;

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaRepository.GetEntityByIdAsync: {ex}");
                return null;
            }
        }

        public override async Task<OperationResult> DeleteEntityAsync(Categoria entity)
        {
            if (entity is null) return OperationResult.GetErrorResult("Input no puede ser null");
            try
            {
                var result = await GetEntityByIdAsync(entity.IdCategoria);
                
                if (result is null) 
                    return OperationResult.GetErrorResult("Categoria no encontrada", code: 404);

                result.Estado = false;

                var resultDelete = await UpdateEntityAsync(result);
                if (!resultDelete.Success) return OperationResult.GetErrorResult("No se elimino correctamente", code: 500);

                return OperationResult.GetSuccesResult(resultDelete, "Categoria eliminada correctamente", code: 200);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoriaRepository.DeleteEntityAsync: {ex}");
                return OperationResult.GetErrorResult("Error eliminando Categoria", code: 500);
            }
        }

        public override async Task<OperationResult> UpdateEntityAsync(Categoria entity)
        {
            try
            {
                var result = await base.UpdateEntityAsync(entity);
                return result.Success
                    ? OperationResult.GetSuccesResult("Actualización exitosa", code: 200)
                    : OperationResult.GetErrorResult("No se actualizo", code: 500);
            }
            catch (Exception ex)
            {
                return OperationResult.GetErrorResult($"Error al actualizar: {ex.Message}");
            }
        }

        public override async Task<OperationResult> SaveEntityAsync(Categoria categoria)
        {
            if (categoria == null) 
                return OperationResult.GetErrorResult("Body is null", code: 400);
            
            if (categoria.Estado == null) 
                return OperationResult.GetErrorResult("Estado no puede ser null", code: 400);

            if (categoria.IdServicio == null)
                return OperationResult.GetErrorResult("IdServicio no puede ser null", code: 400);

            if (string.IsNullOrWhiteSpace(categoria.Descripcion)) 
                return OperationResult.GetErrorResult("Descripcion no puede ser null o tener espacios en blanco", code: 400);

            if (_context.Categorias.Any(cd => cd.Descripcion == categoria.Descripcion)) 
                return OperationResult.GetErrorResult("Esta descripcion ya esta registrado en la base de datos", code: 400);

            return await base.SaveEntityAsync(categoria);
        }
    }
}
    

