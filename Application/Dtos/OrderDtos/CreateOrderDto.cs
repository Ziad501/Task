namespace Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public Guid CartId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; } = null!;
        public PaymentSummeryDto PaymentSummery { get; set; } = null!;
    }
}
