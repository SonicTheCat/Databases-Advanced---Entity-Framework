namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_HospitalDatabase.Data.Models;

    public class VisitationConfig : IEntityTypeConfiguration<Visitation>
    {
        public void Configure(EntityTypeBuilder<Visitation> builder)
        {
            builder.HasKey(v => v.VisitationId);

            builder.Property(v => v.Comments)
                      .HasMaxLength(250)
                      .IsUnicode(true);

            builder.HasOne(v => v.Doctor)
                      .WithMany(d => d.Visitations)
                      .HasForeignKey(v => v.DoctorId);

            builder.HasOne(v => v.Patient)
                      .WithMany(p => p.Visitations)
                      .HasForeignKey(v => v.PatientId);
        }
    }
}