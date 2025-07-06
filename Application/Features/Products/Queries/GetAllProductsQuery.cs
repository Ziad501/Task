using Application.Dtos.ProductDtos;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetAllProductsQuery(string? SearchedItem, int page, int pageSize) : IRequest<PagedList<ProductDto>>;
    
}
