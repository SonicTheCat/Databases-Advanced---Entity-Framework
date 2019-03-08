namespace GazServiz.Data.EntityConfigurations
{
    using GazServiz.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CarConfig : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> car)
        {
            car.HasKey("id").HasName("CarId");

            car.HasAlternateKey(c => c.LicensePlate);

            car.Property(c => c.Model)
               .HasMaxLength(100)
               .IsUnicode()
               .IsRequired();

            car.Property(c => c.LicensePlate)
               .IsRequired();

            car.Property(c => c.Millage);

            car.Property(c => c.OwnerId); 

            car.HasOne(c => c.Owner)
               .WithMany(o => o.Cars)
               .HasForeignKey(c => c.OwnerId);
        }
    }
}