using EShop.API.Dtos;
using MediatR;

namespace EShop.API.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
}
