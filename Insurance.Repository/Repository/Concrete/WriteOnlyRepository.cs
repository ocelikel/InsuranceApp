using Insurance.Model;
using Insurance.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Repository.Repository.Concrete
{
    public class WriteOnlyRepository<T, TId> : IWriteOnlyRepository<T, TId> where T : class, IBaseEntity<TId>
    {
        private readonly IContext context;
        private DbSet<T> entities;
        protected DbSet<T> Table => entities ?? (entities = context.Set<T>());

        public WriteOnlyRepository(IContext context) 
          => this.context = context;

        public TId Add(T entity)
        {
            Table.Add(entity);
            context.SaveChanges();

            return entity.Id;
        }

        public async Task<TId> AddAsync(T entity)
        {
            Table.Add(entity);
            await context.SaveChangesAsync();

            return entity.Id;
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
