namespace EShop.API.Models
{
    public sealed class ProductOption : BaseEntity
    {

        public string Size { get; set; } = string.Empty; 
        public decimal Price { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
