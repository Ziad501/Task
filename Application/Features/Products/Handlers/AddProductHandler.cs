using Application.Dtos;
using Application.Features.Products.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Features.Products.Handlers
{
    public class AddProductHandler(
    ICommandRepository<Product> _cmd,
    IValidator<ProductCreateDto> _validator)
    : IRequestHandler<AddProductCommand, ResultT<ProductCreateDto>>
    {

        public async Task<ResultT<ProductCreateDto>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request.Dto, cancellationToken);
            if (!result.IsValid)
                return new Error("Validation failed", string.Join(", ", result.Errors.Select(e => e.ErrorMessage)));


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
            var dto = request.Dto;
            return ResultT<ProductCreateDto>.Success(dto);
        }
    }
}
