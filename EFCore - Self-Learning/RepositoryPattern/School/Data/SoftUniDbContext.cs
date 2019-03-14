using Microsoft.EntityFrameworkCore;
using SoftUni.Data.EntityConfigurations;
using SoftUni.Data.Extensions;
using SoftUni.Models;
using System;

using System.IO;

namespace SoftUni.Data
{
    public class SoftUniDbContext : DbContext
    {
        public SoftUniDbContext()
        {
        }

        public SoftUniDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Tag> Tags { get; set; }

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
            modelBuilder.ApplyConfiguration(new TeacherConfig());
            modelBuilder.ApplyConfiguration(new CourseTagConfig());

            modelBuilder.Seed(); 
        }
    }
}