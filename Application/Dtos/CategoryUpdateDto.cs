namespace Application.Dtos
{
    public sealed class CategoryUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
