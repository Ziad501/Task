using EShop.API.Dtos;
using MediatR;

namespace EShop.API.Features.Products.Queries
{
    public record GetAllProductsQuery(string? SearchedItem, int page, int pageSize) : IRequest<PagedList<ProductDto>>;
    
}
