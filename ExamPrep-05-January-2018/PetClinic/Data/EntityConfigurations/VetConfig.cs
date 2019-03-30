namespace PetClinic.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetClinic.Models;

    public class VetConfig : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> builder)
        {
            builder.HasAlternateKey(v => v.PhoneNumber); 
        }
    }
}