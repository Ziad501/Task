namespace Domain.Models
{
    public sealed class CartItem
    {
        public Guid ProductId { get; set; }
        public required string ProductTitle { get; set; }
        public decimal Price { get; set; }
        public string Size { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
        public required int Quantity { get; set; }
    }
}
