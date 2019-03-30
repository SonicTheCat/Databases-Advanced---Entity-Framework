namespace PetClinic.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetClinic.Models;

    public class AnimalAidConfig : IEntityTypeConfiguration<AnimalAid>
    {
        public void Configure(EntityTypeBuilder<AnimalAid> builder)
        {
            builder.HasAlternateKey(ai => ai.Name); 
        }
    }
}