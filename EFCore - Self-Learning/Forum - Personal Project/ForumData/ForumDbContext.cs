namespace ForumData
{
    using Microsoft.EntityFrameworkCore;
    using ForumModels;
    using ForumData.EntityConfigurations;
    using ForumData.Extensions;
    using System;
    using System.IO;

    public class ForumDbContext : DbContext
    {
        public ForumDbContext()
        {
        }

        public ForumDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

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
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new CommentConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());

            modelBuilder.Seed();
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Property("LastUpdated").CurrentValue = DateTime.UtcNow; 
                }
            }

            return base.SaveChanges(); 
        }
    }
}