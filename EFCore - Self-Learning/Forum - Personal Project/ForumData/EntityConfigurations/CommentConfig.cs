namespace ForumData.EntityConfigurations
{
    using ForumModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> comment)
        {
            comment.HasKey(c => c.CommentId);

            comment.Property(c => c.CommentId).HasColumnName("CommentID");

            comment.Property(c => c.Content)
                   .IsRequired(true)
                   .IsUnicode(true)
                   .HasMaxLength(500);
        }
    }
}