namespace NonStopMarket.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using NonStopMarket.Models;
    
    public class ProductOrderConfig : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(po => new { po.ProductId, po.OrderId }); 

            builder.HasOne(po => po.Product)
                   .WithMany(p => p.ProductsOrders)
                   .HasForeignKey(po => po.ProductId);

            builder.HasOne(po => po.Order)
                   .WithMany(o => o.ProductsOrders)
                   .HasForeignKey(po => po.OrderId);

        }
    }
}