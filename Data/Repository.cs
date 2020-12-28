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
        protected readonly DbContext databaseContext;

        public Repository(DbContext context)
        {
            databaseContext = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return databaseContext.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return databaseContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }
            await databaseContext.Set<TEntity>().AddAsync(entity);
            await databaseContext.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            databaseContext.Set<TEntity>().Remove(entity);
        }


    }
}
