using Microsoft.EntityFrameworkCore;
using SGHT.Domain.Base;
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

        public virtual Task DeleteEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
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
            OperationResult result = new OperationResult();
            try
            {
                if (entity == null)
                {
                    result.Success = false;
                    result.Message = "La entidad es nula";
                    return result;
                }

                var entry = _context.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    Entity.Attach(entity);
                }

                Entity.Update(entity);

                var saveResult = await _context.SaveChangesAsync();

                result.Success = saveResult > 0;
                result.Message = result.Success ? "Actualizacion exitosa" : "No se realizaron cambios";

            }
            catch (DBConcurrencyException dbex)
            {
                result.Success = false;
                result.Message = "Error de concurrencia de los datos";
            }
            catch (DbUpdateException dbuEx)
            {
                result.Success = false;
                result.Message = "Error al actualizar de los datos";
            }
            catch (Exception ex)
            {

                result.Success = false;
                result.Message = "Ocurrio un error guardando los datos.";
            }
            return result;
        }
    }
}
