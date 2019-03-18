namespace NonStopMarket.Data
{
    using Microsoft.EntityFrameworkCore;

    using EntityConfigurations;
    using NonStopMarket.Models;

    public class NonStopMarketContext : DbContext
    {
        public NonStopMarketContext() { }

        public NonStopMarketContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOrder> ProductsOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfig()); 
            modelBuilder.ApplyConfiguration(new OrderConfig()); 
            modelBuilder.ApplyConfiguration(new ProductOrderConfig()); 
        }
    }
}