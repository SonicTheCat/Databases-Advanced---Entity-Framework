namespace GazServiz.Data.EntityConfigurations
{
    using GazServiz.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class RepairConfig : IEntityTypeConfiguration<Repair>
    {
        public void Configure(EntityTypeBuilder<Repair> repair)
        {
            repair.HasKey("id").HasName("RepairId");

            repair.Property(r => r.IsFixed)
                  .HasDefaultValue(false);

            // repair.Property(r => r.Bill);

            repair.Property(r => r.DateIn);

            // repair.Property(r => r.DateOut);

            repair.Property(r => r.CarId);

            repair.Property(r => r.EmployeeId);

            repair.Property(r => r.ProblemDescription)
                  .HasMaxLength(1000)
                  .IsUnicode();

            repair.HasOne(r => r.Employee)
                  .WithMany(e => e.Repairs)
                  .HasForeignKey(r => r.EmployeeId);

            repair.HasOne(r => r.Car)
                  .WithMany(c => c.Repairs)
                  .HasForeignKey(r => r.CarId);

            var converter = new BoolToStringConverter("Nope", "Yea");

            repair.Property(r => r.IsFixed)
                  .HasConversion(converter);
        }
    }
}