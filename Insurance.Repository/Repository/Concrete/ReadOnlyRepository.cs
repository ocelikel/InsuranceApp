using Insurance.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Insurance.Repository.Repository.Concrete
{
    public class ReadOnlyRepository<T, TId> where T : class, IBaseEntity<TId>
    {
      
        public readonly IContext context;

       
        private DbSet<T> entities;


        public ReadOnlyRepository(IContext context) 
            => this.context = context;

        protected DbSet<T> Table => entities ?? (entities = context.Set<T>());

      
        public static TransactionScope CreateNoLockTransaction()
        {
            var options = new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted };
            return new TransactionScope(TransactionScopeOption.Required, options, TransactionScopeAsyncFlowOption.Enabled);
        }
      
        public virtual IQueryable<T> All() => Table;

        public bool Any(Expression<Func<T, bool>> filter)
        {
            using (CreateNoLockTransaction())
            {
                return Table.Any(filter);
            }
        }

     
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            using (CreateNoLockTransaction())
            {
                return await Table.AnyAsync(filter);
            }
        }

      
        public int Count(Expression<Func<T, bool>> filter)
        {
            using (CreateNoLockTransaction())
            {
                return Table.Count(filter);
            }
        }

       
        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            using (CreateNoLockTransaction())
            {
                return await Table.CountAsync(filter).ConfigureAwait(false);
            }
        }

       
        public T Find(Expression<Func<T, bool>> match)
        {
            using (CreateNoLockTransaction())
            {
                return Table.SingleOrDefault(match);
            }
        }

      
        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            using (CreateNoLockTransaction())
            {
                return Table.Where(match).ToList();
            }
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            using (CreateNoLockTransaction())
            {
                return await Table.Where(match).ToListAsync();
            }
        }

      
        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            using (CreateNoLockTransaction())
            {
                return await Table.SingleOrDefaultAsync(match);
            }
        }

     
        public T Get(TId id)
        {
            using (CreateNoLockTransaction())
            {
                return Table.Find(id);
            }
        }

      
        public async Task<T> GetAsync(TId id)
        {
            using (CreateNoLockTransaction())
            {
                return await Table.FindAsync(id);
            }
        }

    }
}
