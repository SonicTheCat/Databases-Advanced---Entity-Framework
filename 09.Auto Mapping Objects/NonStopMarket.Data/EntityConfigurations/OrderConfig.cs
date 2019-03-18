namespace NonStopMarket.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using NonStopMarket.Models;
    using NonStopMarket.Models.Enums;

    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.Employee)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(o => o.EmployeeId);

            var converter = new EnumToStringConverter<PaymentMethod>();

            builder.Property(o => o.PaymentMethod).HasConversion(converter); 
        }
    }
}