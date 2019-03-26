namespace CarDealer.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class SaleConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasOne(x => x.Car)
                    .WithMany(c => c.Sales)
                    .HasForeignKey(x => x.CarId);

            builder.HasOne(x => x.Customer)
                   .WithMany(c => c.Sales)
                   .HasForeignKey(x => x.CustomerId);
        }
    }
}