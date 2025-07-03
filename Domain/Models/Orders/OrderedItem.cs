
namespace Domain.Models.Orders
{
    public class OrderedItem : BaseEntity
    {
        public ProductOrdered ItemOrdered { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity{ get; set; }
    }
}
