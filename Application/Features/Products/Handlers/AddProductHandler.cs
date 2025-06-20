using EShop.API.Dtos;
using EShop.API.Features.Products.Commands;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using FluentValidation;
using MediatR;

public class AddProductHandler(
    ICommandRepository<Product> _cmd,
    IValidator<ProductCreateDto> _validator)
    : IRequestHandler<AddProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        var product = new Product
        {
            Title = request.Dto.Title,
            Description = request.Dto.Description,
            ImageUrl = request.Dto.ImageUrl,
            CategoryId = request.Dto.CategoryId,
            Options = request.Dto.Options.Select(p => new ProductOption
            {
                Size = p.Size,
                Price = p.Price
            }).ToList()
        };

        await _cmd.AddAsync(product);

        return new ProductDto(
            product.Id,
            product.Title,
            product.Description,
            product.ImageUrl,
            product.CategoryId,
            product.Category?.Name??string.Empty,
            product.Options.Select(o => new ProductOptionDto( o.Size, o.Price)).ToList()
        );
    }
}
