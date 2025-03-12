using Microsoft.EntityFrameworkCore;
using SGHT.Domain.Base;
using SGHT.Domain.Entities;
using SGHT.Domain.Repository;
using SGHT.Persistance.Context;
using System.Data;
using System.Linq.Expressions;

namespace SGHT.Persistance.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly SGHTContext _context;
        private DbSet<TEntity> Entity { get; set; }
        public BaseRepository(SGHTContext  context)
        {
            _context = context;
            Entity = _context.Set<TEntity>();
        }

        [Obsolete("Este metodo no es el correcto, crea uno que actualize el valor 'Estado' a falso.", false)]
        public virtual async Task<OperationResult> DeleteEntityAsync(TEntity entity)
        {
            try
            {
               _context.Remove(entity);
               await _context.SaveChangesAsync();
               return OperationResult.GetSuccesResult("", "Entity deleted successfully.", code: 200);
            }
            catch (Exception ex)
            {
               return OperationResult.GetErrorResult("Entity deleted successfully.", code: 200);
            }
        }

        public virtual async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.AnyAsync(filter);
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await Entity.ToListAsync();
        }

        public virtual async Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            try
            {
                var datos = await Entity.Where(filter).ToListAsync();

                return OperationResult.GetSuccesResult(datos, "Lista de usuarios", code: 200);
            }
            catch (Exception ex)
            {
                return OperationResult.GetErrorResult("Ocurrio un error obteniendo los datos.", code: 500);
            }
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await Entity.FindAsync(id);
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {

            try
            {
                Entity.Add(entity);
                var result = await _context.SaveChangesAsync();
                return OperationResult.GetSuccesResult(result, "usuario guardado correctamente", code: 200);
            }
            catch (Exception ex)
            {
                return OperationResult.GetErrorResult("error guardando usuario", code: 500);
            }
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            if (entity == null) return OperationResult.GetErrorResult("body is null", code: 400);
            try
            {
                _context.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                var saveResult = await _context.SaveChangesAsync();

                return OperationResult.GetSuccesResult(saveResult, "Actualizacion exitosa", code: 200);
            }
            catch (Exception ex)
            {
                return OperationResult.GetErrorResult("No se pudo actualizar", code: 500);
            }
        }
    }
}
