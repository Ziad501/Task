namespace EShop.API.Dtos
{
    public sealed class CartItemsDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public required string ImageUrl { get; set; }
    }
}
