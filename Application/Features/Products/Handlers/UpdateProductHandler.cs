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
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request.Dto, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        if (request.Id != request.Dto.Id)
            throw new ArgumentException("ID mismatch");

        var product = await _query.GetAsync(p => p.Id == request.Id, tracked: true, cancellationToken: cancellationToken);
        if (product is null)
            throw new KeyNotFoundException("Product not found");

        product.Title = request.Dto.Title;
        product.Description = request.Dto.Description;
        product.ImageUrl = request.Dto.ImageUrl;
        product.CategoryId = request.Dto.CategoryId;
        product.Options = request.Dto.Options.Select(o => new ProductOption
        {
            Size = o.Size,
            Price = o.Price
        }).ToList();

        await _cmd.UpdateAsync(product);
    }
}
