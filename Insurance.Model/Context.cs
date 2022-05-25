using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Model
{
    public class Context : DbContext
    {
        protected readonly string connection;

        public Context(string connection)
        {
            this.connection = connection;
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class
            => base.AddRange(entities);


        public Microsoft.EntityFrameworkCore.DbContext Base() => this;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: connection);
        }

    }
}
