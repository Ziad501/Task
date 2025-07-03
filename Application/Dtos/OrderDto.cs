using Domain.Models.Orders;

namespace Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;

        public ShippingAddress ShippingAddress { get; set; } = null!;
        public PaymentSummery PaymentSummery { get; set; } = null!;
        public List<OrderedItem> OrderedItems { get; set; } = [];
    }
}