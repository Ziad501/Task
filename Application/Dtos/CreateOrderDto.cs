using Domain.Models.Orders;

namespace Application.Dtos
{
    public class CreateOrderDto
    {
        public Guid CartId { get; set; }
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public PaymentSummery PaymentSummery { get; set; } = null!;
    }
}
