namespace GazServiz.Data.EntityConfigurations
{
    using GazServiz.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> owner)
        {
            owner.HasKey("ownerId").HasName("OWNERID");

           // owner.HasBaseType<Person>();

            owner.HasAlternateKey(o => o.Mobile);

            owner.Property(o => o.FirstName)
                 .IsRequired()
                 .HasMaxLength(64);

            owner.Property(o => o.LastName)
                 .IsRequired()
                 .HasMaxLength(64); 
        }
    }
}