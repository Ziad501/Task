namespace Application.Dtos
{
    public class OrderedItemDto
    {
        public Guid ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public string ProductSize { get; set; } = "standard";
        public decimal ProductPrice { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
