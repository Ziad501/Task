namespace EShop.API.Models
{
    public sealed class Product : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<ProductOption> Options { get; set; } = new List<ProductOption>();
    }
}
