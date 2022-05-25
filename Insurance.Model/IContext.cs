using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Insurance.Model
{
    public interface IContext
    {
        void AddRange<T>(IEnumerable<T> entities) where T : class;

        Microsoft.EntityFrameworkCore.DbContext Base();

        int SaveChanges();


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));


        DbSet<T> Set<T>() where T : class;

    }
}
