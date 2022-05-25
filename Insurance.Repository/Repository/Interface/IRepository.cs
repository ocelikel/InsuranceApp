using Insurance.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Repository.Repository.Interface
{
    public interface IRepository<T, TId> : IReadOnlyRepository<T, TId>, IWriteOnlyRepository<T, TId> where T : IBaseEntity<TId>
    {
    }
}
