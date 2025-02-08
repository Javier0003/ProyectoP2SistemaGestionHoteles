using System;

namespace SGHT.Domain.Repositorio
{


public interface  IRepositorio <TEntity> where TEntity : class
 {
	Task <TEntity> GetEntityByIdAsync(int id);

	Task UpdateEntityAsync(TEntity entity);

	Task DeleteEntityAsync(TEntity entity);
	Task SaveEntityAsync(TEntity entity);

	Task<List<TEntity>> GetAllAsync();

	Task<OperactionResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);

	task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
}


}