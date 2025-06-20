using EShop.API.Dtos;
using EShop.API.Features.Products.Queries;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShop.API.Features.Products.Handlers
{
    public class GetProductByIdHandler(IQueryRepository<Product> _query)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _query.GetAsync(
                p => p.Id == request.Id,
                include: x => x.Include(p => p.Category)
                               .Include(p => p.Options),tracked:false,
                cancellationToken: cancellationToken);

            if (product == null) return null;

            return new ProductDto(
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
        }
    }

}
