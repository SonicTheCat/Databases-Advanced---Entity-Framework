using FastFood.Models;
using Microsoft.EntityFrameworkCore;

namespace FastFood.Data
{
    public class FastFoodDbContext : DbContext
    {
        public FastFoodDbContext()
        {
        }

        public FastFoodDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderItem>().HasKey(x => new { x.ItemId, x.OrderId });

            builder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(x => x.OrderId);

            builder.Entity<OrderItem>()
               .HasOne(x => x.Item)
               .WithMany(i => i.OrderItems)
               .HasForeignKey(x => x.ItemId);

            builder.Entity<Item>().HasAlternateKey(x => x.Name); 

            builder.Entity<Position>().HasAlternateKey(x => x.Name); 
        }
    }
}