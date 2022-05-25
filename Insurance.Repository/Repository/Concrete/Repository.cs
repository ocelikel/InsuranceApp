using Insurance.Model;
using Insurance.Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Repository.Repository.Concrete
{
    public class Repository<T, TId> : ReadOnlyRepository<T, TId>, IRepository<T, TId> where T : class, IBaseEntity<TId>
    {
        public Repository(IContext context) : base(context)
        {
        }

        public ICollection<T> Add(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return entities;
            }

            Table.AddRange(entities);
            context.SaveChanges();

            return entities;
        }

        public TId Add(T entity)
        {
            Table.Add(entity);
            context.SaveChanges();

            return entity.Id;
        }

        public async Task<ICollection<T>> AddAsync(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return entities;
            }

            await Table.AddRangeAsync(entities);
            await context.SaveChangesAsync();

            return entities;
        }

        public async Task<TId> AddAsync(T entity)
        {
            Table.Add(entity);
            await context.SaveChangesAsync();

            return entity.Id;
        }

        public void Delete(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }

            Table.RemoveRange(entities);
            context.SaveChanges();
        }

        public int Delete(T entity)
        {
            Table.Remove(entity);

            return context.SaveChanges();
        }

        public bool Delete(TId key)
        {
            var entity = Table.Find(key);

            if (entity == null)
            {
                return false;
            }

            Table.Remove(entity);

            return context.SaveChanges() > 0;
        }

        public async Task DeleteAsync(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return;
            }

            Table.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            Table.Remove(entity);

            return await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(TId key)
        {
            var entity = await Table.FindAsync(key);

            if (entity == null)
            {
                return false;
            }

            return await DeleteAsync(entity) > 0;
        }

        public ICollection<T> Update(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return entities;
            }

            Table.UpdateRange(entities);
            context.SaveChanges();

            return entities;
        }

        public T Update(TId key, T entity)
        {
            if (entity == null)
            {
                return null;
            }

            var item = Table.Find(key);

            if (item == null)
            {
                return null;
            }

            Table.Update(entity);
            context.SaveChanges();

            return item;
        }

        public async Task<ICollection<T>> UpdateAsync(ICollection<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return entities;
            }

            Table.UpdateRange(entities);
            await context.SaveChangesAsync();

            return entities;
        }

        public async Task<T> UpdateAsync(TId key, T entity)
        {
            if (entity == null)
            {
                return null;
            }

            var item = Table.Find(key);

            if (item == null)
            {
                return null;
            }

            Table.Update(entity);
            await context.SaveChangesAsync();

            return item;
        }
    }
}
