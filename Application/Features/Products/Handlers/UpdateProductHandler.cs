using Domain.Abstractions;
using EShop.API.Dtos;
using EShop.API.Features.Products.Commands;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using FluentValidation;
using MediatR;

public class UpdateProductHandler(
    IQueryRepository<Product> _query,
    ICommandRepository<Product> _cmd,
    IValidator<ProductUpdateDto> _validator)
    : IRequestHandler<UpdateProductCommand, ResultT<ProductDto>>
{

    public async Task<ResultT<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)

    {
        var result = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!result.IsValid)
            return new Error("validation_error", string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));

        if (request.Id != request.Dto.Id)
            return Errors.IdMissMatch;

        var existing = await _query.GetAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
        if (existing is null)
            return Errors.NotFound;

        var product = new Product
        {
            Id = request.Id,
            Title = request.Dto.Title,
            Description = request.Dto.Description,
            ImageUrl = request.Dto.ImageUrl,
            CategoryId = request.Dto.CategoryId,
            Options = request.Dto.Options.Select(o => new ProductOption
            {
                Size = o.Size,
                Price = o.Price
            }).ToList()
        };
        await _cmd.UpdateAsync(product);
        var dto = new ProductDto(
            product.Id,
            product.Title,
            product.Description,
            product.ImageUrl,
            product.CategoryId,
            product.Category?.Name ?? string.Empty,
            product.Options.Select(o => new ProductOptionDto(o.Size, o.Price)).ToList()
        );
        return ResultT<ProductDto>.Success(dto);
    }
}
