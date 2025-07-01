using Application.Dtos;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Handlers
{
    public class GetProductByIdHandler(IQueryRepository<Product> _query)
    : IRequestHandler<GetProductByIdQuery, ResultT<ProductDto>>
    {
        public async Task<ResultT<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _query.GetAsync(
                p => p.Id == request.Id,
                include: x => x.Include(p => p.Category)
                               .Include(p => p.Options),
                cancellationToken: cancellationToken);

            if (product is null) return Errors.NotFound;

            var dto= new ProductDto(
                product.Id,
                product.Title,
                product.Description,
                product.ImageUrl,
                product.CategoryId,
                product.Category?.Name ?? string.Empty,
                product.Options.Select(o => new ProductOptionDto(
                    o.Size,
                    o.Price)).ToList()
            );
            return ResultT<ProductDto>.Success(dto);
        }
    }

}
