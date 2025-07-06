namespace Application.Dtos.OrderDtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;

        public ShippingAddressDto ShippingAddress { get; set; } = null!;
        public PaymentSummeryDto PaymentSummery { get; set; } = null!;
        public List<OrderedItemDto> OrderedItems { get; set; } = [];
    }
}