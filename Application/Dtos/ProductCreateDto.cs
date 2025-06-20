using EShop.API.Models;

namespace EShop.API.Dtos
{
    public sealed class ProductCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public List<ProductOptionCreateDto> Options { get; set; } = new();
    }

}
