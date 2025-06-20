namespace EShop.API.Models
{
    public sealed class Cart
    {
        public required Guid Id { get; set; }
        public List<CartItem> Items { get; set; } = [];
    }
}
