namespace ForumData.EntityConfigurations
{
    using ForumModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> post)
        {
            post.HasKey(p => p.PostId);

            post.Property(p => p.PostId).HasColumnName("PostID");

            post.Property(p => p.Title)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(50);

            post.Property(p => p.Content)
                .IsRequired(true)
                .IsUnicode(true)
                .HasMaxLength(1000);

            post.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            post.Property<DateTime>("LastUpdated")
                .HasDefaultValue(); 
        }
    }
}