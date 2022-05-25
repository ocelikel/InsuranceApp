using Insurance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Repository.Repository.Interface
{
    public interface IWriteOnlyRepository<T, TId> where T : IBaseEntity<TId>
    {
        TId Add(T entity);

        Task<TId> AddAsync(T entity);

        int Delete(T entity);

        bool Delete(TId key);

        Task<int> DeleteAsync(T entity);

        Task<bool> DeleteAsync(TId key);

        T Update(TId key, T entity);

        Task<T> UpdateAsync(TId key, T entity);
    }
}
