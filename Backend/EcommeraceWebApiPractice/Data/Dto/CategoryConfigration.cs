using Ecom.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcommeraceWebApiPractice.Data.Dto
{
    public class CategoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(x => x.description).HasMaxLength(255).IsRequired();
            builder.Property(x => x.name).IsRequired().HasMaxLength(60);
            builder.HasData(new Category
            {
                id = 1,
                name = "Electronics",
                description = "Electronic devices"
            },

                new Category
                {
                    id = 2,
                    name = "Fashion",
                    description = "Clothing and accessories"
                },

                new Category
                {
                    id = 3,
                    name = "Home",
                    description = "Home furniture"
                },

                new Category
                {
                    id = 4,
                    name = "Sports",
                    description = "Sports products"
                },

                new Category
                {
                    id = 5,
                    name = "Beauty",
                    description = "Beauty products"
                });

                //builder.HasMany(p => p.Products)
            //    .WithOne(p => p.Category)
            //    .HasForeignKey(p => p.CategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
