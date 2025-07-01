namespace Application.Dtos
{
    public sealed class CartDto
    {
        public Guid Id { get; set; }
        public List<CartItemsDto> Items { get; set; } = [];
    }
}
