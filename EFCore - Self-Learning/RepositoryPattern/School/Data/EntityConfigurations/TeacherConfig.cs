using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUni.Models;

namespace SoftUni.Data.EntityConfigurations
{
    public class TeacherConfig : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasMany(x => x.Courses)
                   .WithOne(x => x.Teacher)
                   .HasForeignKey(x => x.TeacherId); 
        }
    }
}