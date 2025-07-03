using Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentConfigurations.OrderConfig
{
    public class OrderedItemConfiguration : IEntityTypeConfiguration<OrderedItem>
    {
        public void Configure(EntityTypeBuilder<OrderedItem> builder)
        {
            builder.OwnsOne(oi => oi.ItemOrdered, io =>
            {
                io.WithOwner();
                io.Property(p => p.ProductID).IsRequired();
                io.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
                io.Property(p => p.ProductImage).IsRequired();
                io.Property(p => p.ProductSize).IsRequired();
                io.Property(p => p.ProductPrice).HasColumnType("decimal(18,2)");
            });

            builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
            builder.Property(oi => oi.Quantity).IsRequired();
        }
    }
}