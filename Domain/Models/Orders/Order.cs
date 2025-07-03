namespace Domain.Models.Orders
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string BuyerEmail { get; set; } = null!;
        public ShippingAddress ShippingAddress { get; set; } = null!;
        public PaymentSummery PaymentSummery { get; set; } = null!;
        public List<OrderedItem> OrderedItems { get; set; } = [];
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.pending;
        public string? PaymentIntentId { get; set; }
    }
}
