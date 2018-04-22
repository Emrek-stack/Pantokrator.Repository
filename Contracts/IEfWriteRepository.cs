using System.Threading.Tasks;

namespace Pantokrator.Data.Sql.Contracts
{
    public interface IEfWriteRepository<TEntity> where TEntity : class, new()

    {

    #region Sync Methods        

    TEntity Insert(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);

    #endregion

    #region Async Methods        

    Task InsertAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);

    #endregion

    }
}