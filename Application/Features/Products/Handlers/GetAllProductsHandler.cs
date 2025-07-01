using Application.Dtos;
using Application.Features.Products.Queries;
using Application.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Handlers
{
    public class GetAllProductsHandler(IQueryRepository<Product> _query)
         : IRequestHandler<GetAllProductsQuery, PagedList<ProductDto>>
    {
        public async Task<PagedList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Product> products = _query.GetAll(include: p=>p.Include(c=>c.Category).Include(o=>o.Options));
            if(!string.IsNullOrWhiteSpace(request.SearchedItem))
            {
                products = products.Where(p => p.Title.Contains(request.SearchedItem));
            }
            var productResponse = products
                .Select(p => new ProductDto(
                p.Id,
                p.Title,
                p.Description,
                p.ImageUrl,
                p.CategoryId,
                p.Category != null ? p.Category.Name : string.Empty,
                p.Options.Select(o => new ProductOptionDto(
                    o.Size,
                    o.Price)).ToList()
            ));
            return await PagedList<ProductDto>.CreateAsync(productResponse, request.page, request.pageSize);

        }
    }
}
