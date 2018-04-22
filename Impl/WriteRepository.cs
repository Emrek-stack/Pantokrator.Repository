using System;
using System.Threading.Tasks;
using Frost.Data.Sql.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Frost.Data.Sql.Impl
{
    public abstract class WriteRepository<TEntity, TContext> : IEfWriteRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, IDisposable
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _entities;


        #region constructor
        protected WriteRepository(TContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
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
        public TEntity Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Add(entity);
            SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();

            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
            SaveChanges();
            return entity;
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
            await SaveChangesAsync();
        }
        #endregion
    }

}