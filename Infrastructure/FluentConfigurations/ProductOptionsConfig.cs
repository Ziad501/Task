using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentConfigurations
{
    internal sealed class ProductOptionsConfig : IEntityTypeConfiguration<ProductOption>
    {
        public void Configure(EntityTypeBuilder<ProductOption> builder)
        {
            builder.Property(o => o.Size)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(o => o.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.HasOne(o => o.Product)
                   .WithMany(p => p.Options)
                   .HasForeignKey(o => o.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                new ProductOption
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Size = "100x210 cm",
                    Price = 3500,
                    ProductId = Guid.Parse("33333333-3333-3333-3333-333333333333")
                },
                new ProductOption
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Size = "5 Litre",
                    Price = 750,
                    ProductId = Guid.Parse("44444444-4444-4444-4444-444444444444")
                }
            );

        }
    }
}
