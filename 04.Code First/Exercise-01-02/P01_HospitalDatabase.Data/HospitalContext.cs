namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using System.IO;

    using P01_HospitalDatabase.Data.Models;
    using P01_HospitalDatabase.Data.EntityConfiguration;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
        {
        }

        public HospitalContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(File.ReadAllText("../../../../../ConnectionString.txt"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PatientConfig());
            builder.ApplyConfiguration(new DoctorConfig());
            builder.ApplyConfiguration(new VisitationConfig());
            builder.ApplyConfiguration(new DiagnoseConfig());
            builder.ApplyConfiguration(new MedicamentConfig());
            builder.ApplyConfiguration(new PatientMedicamentConfig());
        }
    }
}