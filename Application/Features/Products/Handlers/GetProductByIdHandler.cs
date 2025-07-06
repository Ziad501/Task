using Application.Dtos.ProductDtos;
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

            var dto = new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty,
                Options = product.Options.Select(o => new ProductOptionDto { Size = o.Size, Price = o.Price }).ToList()
            };
            return ResultT<ProductDto>.Success(dto);
        }
    }

}
