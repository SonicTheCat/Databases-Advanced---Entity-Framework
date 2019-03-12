namespace P01_BillsPaymentSystem.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    using P01_BillsPaymentSystem.Data.Models;

    public class PaymentMethodConfig : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.HasOne(pm => pm.User)
                   .WithMany(u => u.PaymentMethods)
                   .HasForeignKey(pm => pm.UserId);

            builder.HasOne(pm => pm.CreditCard)
                   .WithOne(cc => cc.PaymentMethod)
                   .HasForeignKey<PaymentMethod>(pm => pm.CreditCardId);

            builder.HasOne(pm => pm.BankAccount)
                   .WithOne(ba => ba.PaymentMethod)
                   .HasForeignKey<PaymentMethod>(pm => pm.BankAccountId);

            var converter = new EnumToStringConverter<PaymentMethodType>();
            builder.Property(pm => pm.Type)
                   .HasConversion(converter);
        }
    }
}