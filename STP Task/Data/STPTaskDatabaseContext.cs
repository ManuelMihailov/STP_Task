using Data.Configuration;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class STPTaskDatabaseContext : DbContext
    {
        public STPTaskDatabaseContext()
        {
        }

        public STPTaskDatabaseContext(DbContextOptions<STPTaskDatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(@"Data Source=tcp:cocktailmagician.database.windows.net,1433;Initial Catalog=cocktailMagician;User Id=MagicianVM@cocktailmagician.database.windows.net;Password=123abcVM;");
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=STPTask;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new OfficeConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
