using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Frost.Data.Sql.Extensions;

namespace Frost.Data.Sql.Contracts
{
    public interface IEfReadRepository<TEntity> where TEntity : class, new()
    {
        #region Sync Methods
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