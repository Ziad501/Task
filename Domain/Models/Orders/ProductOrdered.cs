namespace Domain.Models.Orders
{
    public class ProductOrdered
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public string ProductSize { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
    }
}
