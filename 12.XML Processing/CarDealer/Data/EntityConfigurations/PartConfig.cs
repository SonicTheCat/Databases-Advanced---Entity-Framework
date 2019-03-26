namespace CarDealer.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PartConfig : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasOne(x => x.Supplier)
                   .WithMany(s => s.Parts)
                   .HasForeignKey(x => x.SupplierId); 
        }
    }
}