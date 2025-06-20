using EShop.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EShop.API.FluentConfigurations
{
    internal sealed class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            builder.Property(p => p.ImageUrl)
                   .HasMaxLength(300);

            builder.HasIndex(p => p.Title);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(p => p.Options)
                   .WithOne(o => o.Product)
                   .HasForeignKey(o => o.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                new Product
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Steel Security Door",
                    Description = "High-quality steel door for security",
                    ImageUrl = "images/door1.jpg",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111")
                },
                new Product
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Title = "Interior White Paint",
                    Description = "Matte white paint for walls",
                    ImageUrl = "images/paint1.jpg",
                    CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222")
                }
            );

        }
    }
}
