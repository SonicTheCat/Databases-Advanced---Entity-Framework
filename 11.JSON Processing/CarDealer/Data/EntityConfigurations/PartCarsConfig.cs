namespace CarDealer.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PartCarsConfig : IEntityTypeConfiguration<PartCars>
    {
        public void Configure(EntityTypeBuilder<PartCars> builder)
        {
            builder.HasKey(x => new { x.CarId, x.PartId });

            builder.HasOne(pc => pc.Car)
                   .WithMany(c => c.PartCars)
                   .HasForeignKey(pc => pc.CarId);

            builder.HasOne(pc => pc.Part)
                   .WithMany(c => c.PartCars)
                   .HasForeignKey(pc => pc.PartId);
        }
    }
}