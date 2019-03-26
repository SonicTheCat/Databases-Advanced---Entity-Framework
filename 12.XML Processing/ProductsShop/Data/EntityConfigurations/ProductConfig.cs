namespace ProductsShop.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using ProductsShop.Models;

    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Buyer)
                   .WithMany(u => u.BoughtProducts)
                   .HasForeignKey(p => p.BuyerId);

            builder.HasOne(p => p.Seller)
                  .WithMany(u => u.SellingProducts)
                  .HasForeignKey(p => p.SellerId);
        }
    }
}