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

        protected Repository(DbContext context)
        {
            _databaseContext = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _databaseContext.Set<TEntity>().ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public TEntity Get(int id)
        {
            try
            {
                return _databaseContext.Set<TEntity>().Find(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
                }
                await _databaseContext.Set<TEntity>().AddAsync(entity);
                await _databaseContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
                }
                _databaseContext.Set<TEntity>().Update(entity);
                await _databaseContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task RemoveAsync(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException($"{nameof(RemoveAsync)} entity must not be null");
                }
                _databaseContext.Set<TEntity>().Remove(entity);
                await _databaseContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
