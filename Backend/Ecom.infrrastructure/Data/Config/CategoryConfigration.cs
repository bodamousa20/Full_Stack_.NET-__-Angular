using Ecom.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.infrrastructure.Data.Config
{
    public class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(x => x.description).HasMaxLength(255).IsRequired();
            builder.Property(x => x.name).IsRequired().HasMaxLength(60);

            //builder.HasMany(p => p.Products)
            //    .WithOne(p => p.Category)
            //    .HasForeignKey(p => p.CategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
