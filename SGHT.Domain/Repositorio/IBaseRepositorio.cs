using SGHT.Domain.Base;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SGHT.Domain.Repositorio
{
	public interface IBaseRepositorio <TEntity> where TEntity : class
	{
		Task<TEntity> GetEntityByIdAsync(int id);

		Task UpdateEntityAsync(TEntity entity);

		Task DeleteEntityAsync(TEntity entity);
		Task SaveEntityAsync(TEntity entity);

		Task<List<TEntity>> GetAllAsync();

		Task<OperationResult> GetAllAsync(Expression<Func<TEntity, bool>> filter);

		Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
	}
}