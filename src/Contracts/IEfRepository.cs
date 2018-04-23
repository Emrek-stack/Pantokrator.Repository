using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pantokrator.Repository.Extensions;

namespace Pantokrator.Repository.Contracts
{
    public interface IEfRepository<TEntity> where TEntity : class
    {
        #region Sync Methods

        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(TEntity entity);

        IEnumerable<TEntity> GetAll();
        TEntity GetBy(Func<TEntity, bool> predicate);
        IList<TEntity> GetAllBy(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> GetAllBy(
            Expression<Func<TEntity, bool>> filter = null,
            string[] includePaths = null,
            int? page = 0, int? pageSize = null,
            params SortExpression<TEntity>[] sortExpressions);
        #endregion

        #region Async Methods

        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IReadOnlyList<TEntity>> GetAllByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IReadOnlyList<TEntity>> GetAllByAsync(
            Expression<Func<TEntity, bool>> predicate,
            string[] includePaths = null,
            int? page = 0, int? pageSize = null,
            params SortExpression<TEntity>[] sortExpressions);
        #endregion
    }
}