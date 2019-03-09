namespace P01_StudentSystem.Data.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class HomeworkConfig : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(h => h.HomeworkId);

            builder.HasOne(h => h.Course)
                   .WithMany(c => c.HomeworkSubmissions)
                   .HasForeignKey(h => h.CourseId);

            builder.HasOne(h => h.Student)
                   .WithMany(s => s.HomeworkSubmissions)
                   .HasForeignKey(h => h.StudentId); 

            builder.Property(h => h.Content)
                   .IsUnicode(false); 
        }
    }
}