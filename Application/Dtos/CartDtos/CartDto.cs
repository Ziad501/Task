namespace Application.Dtos.CartDtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public List<CartItemsDto> Items { get; set; } = [];
    }
}
