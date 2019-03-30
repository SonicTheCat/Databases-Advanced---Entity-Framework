namespace PetClinic.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetClinic.Models;

    public class ProcedureAnimalAidConfig : IEntityTypeConfiguration<ProcedureAnimalAid>
    {
        public void Configure(EntityTypeBuilder<ProcedureAnimalAid> builder)
        {
            builder.HasKey(x => new { x.AnimalAidId, x.ProcedureId });

            builder.HasOne(x => x.AnimalAid)
                   .WithMany(ai => ai.AnimalAidProcedures)
                   .HasForeignKey(x => x.AnimalAidId);

            builder.HasOne(x => x.Procedure)
                   .WithMany(p => p.ProcedureAnimalAids)
                   .HasForeignKey(x => x.ProcedureId);
        }
    }
}