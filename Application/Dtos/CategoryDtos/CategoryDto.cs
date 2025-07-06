using Application.Dtos.ProductDtos;

namespace Application.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductDto> Products { get; set; } = [];
    }
}
