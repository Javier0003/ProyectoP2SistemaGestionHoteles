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

        public virtual async Task<OperationResult> DeleteEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();
            try
            {
               _context.Remove(entity);
               await _context.SaveChangesAsync();
               result.Success = true;
               result.Message = "Entity deleted successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"An error occurred: {ex.Message}";
            }

            return result;
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
            OperationResult result = new OperationResult();

            try
            {
                var datos = await Entity.Where(filter).ToListAsync();

                result.Data = datos;
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "Ocurrio un error obteniendo los datos.";
            }

            return result;
        }

        public virtual async Task<TEntity> GetEntityByIdAsync(int id)
        {
            return await Entity.FindAsync(id);
        }

        public virtual async Task<OperationResult> SaveEntityAsync(TEntity entity)
        {
            OperationResult result = new OperationResult();

            try
            {
                Entity.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Ocurrio un error guardando los datos.";
            }
            return result;
        }

        public virtual async Task<OperationResult> UpdateEntityAsync(TEntity entity)
        {
            var result = new OperationResult();
            try
            {
                _context.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                var saveResult = await _context.SaveChangesAsync();

                result.Success = saveResult > 0;
                result.Message = result.Success ? "Actualización exitosa" : "No se realizaron cambios";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error al actualizar: {ex.Message}";
            }

            return result;
        }
    }
}
