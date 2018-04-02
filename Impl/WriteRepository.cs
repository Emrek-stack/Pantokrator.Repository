using System;
using System.Threading.Tasks;
using Frost.Data.Sql.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Frost.Data.Sql.Impl
{
    public abstract class WriteRepository<TEntity, TContext> : IEfWriteRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext, IDisposable
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _entities;


        #region constructor
        protected WriteRepository(TContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            //if (!(unitOfWork is EfUnitOfWork<TContext> efUnitOfWork)) throw new Exception("Must be EfUnitOfWork"); // TODO: Typed exception
            //_entities = efUnitOfWork.GetDbSet<TEntity>();
        }
        #endregion

        #region Private Methods
        private async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        private int SaveChanges()
        {
            return _context.SaveChanges();
        }
        #endregion

        #region Sync Methods              
        public void Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            //_entities.Remove(entity);
            SaveChanges();
        }
        #endregion

        #region Async Methods                  
        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _entities.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            //_entities.Remove(entity);
            await SaveChangesAsync();
        }
        #endregion
    }

}