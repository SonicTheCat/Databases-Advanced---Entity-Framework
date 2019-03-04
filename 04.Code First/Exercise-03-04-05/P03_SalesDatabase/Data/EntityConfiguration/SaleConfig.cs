namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;

    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(s => s.SaleId);

            builder.HasOne(s => s.Product)
                   .WithMany(p => p.Sales)
                   .HasForeignKey(s => s.ProductId);

            builder.HasOne(s => s.Customer)
                  .WithMany(c => c.Sales)
                  .HasForeignKey(s => s.CustomerId);

            builder.HasOne(s => s.Store)
                  .WithMany(s => s.Sales)
                  .HasForeignKey(s => s.StoreId);

            builder.Property(s => s.Date)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}