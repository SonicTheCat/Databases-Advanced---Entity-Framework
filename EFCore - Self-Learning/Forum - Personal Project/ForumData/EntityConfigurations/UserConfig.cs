namespace ForumData.EntityConfigurations
{
    using ForumModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(u => u.UserId);

            user.Property(u => u.UserId).HasColumnName("UserID");

            user.Property(u => u.Username)
                .IsRequired(true)
                .HasMaxLength(16)
                .IsUnicode(true);

            user.Property(u => u.Status)
                .HasMaxLength(32)
                .IsUnicode(true);

            user.HasMany(u => u.Posts)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId);

            user.HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .HasForeignKey(c => c.AuthorId);

            //Shadow property
            user.Property<DateTime>("RegistrationDate")
                .HasDefaultValueSql("GETDATE()");
        }
    }
}