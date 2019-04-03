﻿namespace VaporStore.Data
{
    using Microsoft.EntityFrameworkCore;
    using VaporStore.Data.Models;

    public class VaporStoreDbContext : DbContext
    {
        public VaporStoreDbContext()
        {
        }

        public VaporStoreDbContext(DbContextOptions options)
            : base(options)
        {
        }

       public DbSet<Purchase> Purchases { get; set; }
       public DbSet<Card> Cards { get; set; }
       public DbSet<Game> Games { get; set; }
       public DbSet<GameTag> GameTags { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<Tag> Tags { get; set; }
       public DbSet<Developer> Developers { get; set; }
       public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<GameTag>().HasKey(x => new { x.TagId, x.GameId });

            model.Entity<GameTag>().HasOne(x => x.Game)
                .WithMany(g => g.GameTags)
                .HasForeignKey(x => x.GameId);

            model.Entity<GameTag>().HasOne(x => x.Tag)
              .WithMany(t => t.GameTags)
              .HasForeignKey(x => x.TagId);
        }
    }
}