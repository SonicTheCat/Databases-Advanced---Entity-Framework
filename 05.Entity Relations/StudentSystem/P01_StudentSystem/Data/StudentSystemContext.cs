namespace P01_StudentSystem.Data
{
    using System;
    using System.IO;

    using Microsoft.EntityFrameworkCore;

    using P01_StudentSystem.Data.EntityConfigurations;
    using P01_StudentSystem.Data.Extensions;
    using P01_StudentSystem.Data.Models;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext() {}

        public StudentSystemContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Homework> HomeworkSubmissions { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }


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
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new HomeworkConfig());
            modelBuilder.ApplyConfiguration(new ResourceConfig());
            modelBuilder.ApplyConfiguration(new StudentCoursesConfig());

            //Do not submit in judge the Seed Method
            modelBuilder.Seed(); 
        }
    }
}