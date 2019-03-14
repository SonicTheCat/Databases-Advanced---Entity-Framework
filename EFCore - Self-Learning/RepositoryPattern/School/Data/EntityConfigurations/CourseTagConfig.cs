using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUni.Models;

namespace SoftUni.Data.EntityConfigurations
{
    public class CourseTagConfig : IEntityTypeConfiguration<CourseTag>
    {
        public void Configure(EntityTypeBuilder<CourseTag> builder)
        {
            builder.HasKey(x => new { x.TagId, x.CourseId });

            builder.HasOne(ct => ct.Tag)
                   .WithMany(t => t.CourseTags)
                   .HasForeignKey(ct => ct.TagId);

            builder.HasOne(ct => ct.Course)
                   .WithMany(c => c.CourseTags)
                   .HasForeignKey(ct => ct.CourseId);
        }
    }
}