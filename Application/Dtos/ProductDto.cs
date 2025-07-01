namespace Application.Dtos
{
    public record ProductDto(

        Guid Id,
        string Title,
        string Description,
        string ImageUrl,
        Guid CategoryId,
        string CategoryName, 
        List<ProductOptionDto> Options
    );

}
