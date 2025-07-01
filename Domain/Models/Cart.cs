namespace Domain.Models
{
    public sealed class Cart : BaseEntity
    {
        public List<CartItem> Items { get; set; } = [];
    }
}
