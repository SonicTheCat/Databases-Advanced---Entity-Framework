namespace ForumData.EntityConfigurations
{
    using ForumModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> category)
        {
            category.HasKey(c => c.CategoryId);

            //category.HasMany(c => c.Posts)
            //        .WithOne(p => p.Category)
            //        .HasForeignKey(p => p.CategoryId)
            //        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}