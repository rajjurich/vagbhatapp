using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace vagbhatapp.Data.Core
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<T> RemoveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        IQueryable<T> Get();
        Task<T> GetAsync(string key);
    }
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly EntitiesContext entitiesContext;

        public EntityRepository(EntitiesContext entitiesContext)
        {
            this.entitiesContext = entitiesContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await entitiesContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await entitiesContext.Set<T>().Where(predicate).CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await entitiesContext.Set<T>().CountAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => entitiesContext.Entry(entity).State = EntityState.Detached);
            return entity;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return entitiesContext.Set<T>().Where(predicate);
        }

        public IQueryable<T> Get()
        {
            return entitiesContext.Set<T>();
        }

        public async Task<T> GetAsync(string key)
        {
            return await entitiesContext.Set<T>().FindAsync(key);
        }

        public async Task<T> RemoveAsync(T entity)
        {
            await Task.Run(() => entitiesContext.Set<T>().Remove(entity));
            return entity;
        }
    }
}
