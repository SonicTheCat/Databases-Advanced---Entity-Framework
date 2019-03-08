namespace GazServiz.Data
{
    using GazServiz.Data.EntityConfigurations;
    using GazServiz.Models;
    using GazServiz.Data.Extensions;

    using Microsoft.EntityFrameworkCore;

    using System;
    using System.IO;

    public class GarageDbContext : DbContext
    {
        public GarageDbContext()
        {
        }

        public GarageDbContext(DbContextOptions optionsBuilder)
            : base(optionsBuilder)
        {
        }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Employee> Emoloyees { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Repair> Repairs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var path = Path.Combine(Environment.CurrentDirectory, @"C:\Users\Pavel\ConnectionString.txt");
                optionsBuilder.UseSqlServer(File.ReadAllText(path));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            modelBuilder.ApplyConfiguration(new CarConfig());
            modelBuilder.ApplyConfiguration(new EmployeeConfig());
            modelBuilder.ApplyConfiguration(new OwnerConfig());
            modelBuilder.ApplyConfiguration(new RepairConfig());
           
            modelBuilder.Seed(); 
        }
    }
}