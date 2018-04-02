using System.Threading.Tasks;

namespace Frost.Data.Sql.Contracts
{
    public interface IEfWriteRepository<in TEntity> where TEntity : BaseEntity
    {

        #region Sync Methods        
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        #endregion

        #region Async Methods        
        Task InsertAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        #endregion
    }
}