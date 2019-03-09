namespace P01_StudentSystem.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class ResourceConfig : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(r => r.ResourceId);

            builder.HasOne(r => r.Course)
                   .WithMany(c => c.Resources)
                   .HasForeignKey(r => r.CourseId);

            builder.Property(r => r.Name)
                   .HasMaxLength(50)
                   .IsUnicode();

            builder.Property(r => r.Url)
                   .IsUnicode(false);
        }
    }
}