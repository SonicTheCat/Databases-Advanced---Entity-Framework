namespace P01_HospitalDatabase.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_HospitalDatabase.Data.Models;

    public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
    {
        public void Configure(EntityTypeBuilder<PatientMedicament> builder)
        {
            builder.HasKey(x => new { x.PatientId, x.MedicamentId });

            builder.HasOne(x => x.Patient)
              .WithMany(p => p.Prescriptions)
              .HasForeignKey(x => x.PatientId);

            builder.HasOne(x => x.Medicament)
              .WithMany(m => m.Prescriptions)
              .HasForeignKey(x => x.MedicamentId);
        }
    }
}