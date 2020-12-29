using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VSApi.Interfaces;

namespace VSApi.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _databaseContext;

        public Repository(DbContext context)
        {
            _databaseContext = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _databaseContext.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return _databaseContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }
            await _databaseContext.Set<TEntity>().AddAsync(entity);
            await _databaseContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Update(entity);
            await _databaseContext.SaveChangesAsync();

            return entity;
        }

        public void Remove(TEntity entity)
        {
            _databaseContext.Set<TEntity>().Remove(entity);
        }


    }
}
