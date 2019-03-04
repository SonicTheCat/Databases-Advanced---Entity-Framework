namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_HospitalDatabase.Data.Models;

    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.PatientId);

            builder.Property(p => p.FirstName)
                   .HasMaxLength(50)
                   .IsUnicode(true);

            builder.Property(p => p.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(true);

            builder.Property(p => p.Address)
                   .HasMaxLength(250)
                   .IsUnicode(true);

            builder.Property(p => p.Email)
                  .HasMaxLength(80)
                  .IsUnicode(false);
        }
    }
}