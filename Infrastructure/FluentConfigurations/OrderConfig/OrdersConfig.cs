using Domain.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.FluentConfigurations.OrderConfig
{
    public class OrdersConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.BuyerEmail).IsRequired().HasMaxLength(100);
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(o => o.Status).HasConversion(x=>x.ToString(),x=>(OrderStatus)Enum.Parse(typeof(OrderStatus), x));
            builder.Property(o => o.PaymentIntentId).HasMaxLength(100);
            builder.Property(o=>o.OrderDate).HasConversion(
                x => x.ToUniversalTime(),
                x => DateTime.SpecifyKind(x, DateTimeKind.Utc)
            );

            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.WithOwner();
                sa.Property(p => p.Name).IsRequired();
                sa.Property(p => p.Line1).IsRequired();
                sa.Property(p => p.City).IsRequired();
                sa.Property(p => p.State).IsRequired();
                sa.Property(p => p.PostalCode).IsRequired();
                sa.Property(p => p.Country).IsRequired();
            });

            builder.OwnsOne(o => o.PaymentSummery, ps =>
            {
                ps.WithOwner();
                ps.Property(p => p.Last4).IsRequired().HasMaxLength(4);
                ps.Property(p => p.ExpMonth).IsRequired().HasMaxLength(2);
                ps.Property(p => p.ExpYear).IsRequired().HasMaxLength(2);
            });

            builder.HasMany(o => o.OrderedItems)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}