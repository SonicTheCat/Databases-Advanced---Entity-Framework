namespace P03_SalesDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;

    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);

            builder.Property(p => p.Name)
                   .HasMaxLength(100)
                   .IsUnicode();

            builder.Property(c => c.Email)
                 .HasMaxLength(80)
                 .IsUnicode(false);
        }
    }
}
