using Insurance.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Insurance.Model
{
    public class InsuranceContext : Context, IContext
    {
       
        public InsuranceContext(string connection) : base(connection)
        {
        }

        public DbSet<UserInsurance> UserInsurance { get; set; }
        public DbSet<Offer> Offer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInsurance>().Property(p => p.Plate)
            .HasMaxLength(20);
            modelBuilder.Entity<UserInsurance>().Property(p => p.LicenseSerialCode)
             .HasMaxLength(50);
            modelBuilder.Entity<UserInsurance>().Property(p => p.LicenseSerialNo)
               .HasMaxLength(50);
            modelBuilder.Entity<UserInsurance>().Property(p => p.IdentityNumber)
               .HasMaxLength(11);
        }
    }
}
