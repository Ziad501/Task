namespace Application.Dtos
{
    public sealed class ProductUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public List<ProductOptionUpdateDto> Options { get; set; } = new();
    }
}
