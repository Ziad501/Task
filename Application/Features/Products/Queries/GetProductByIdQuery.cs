using Domain.Abstractions;
using EShop.API.Dtos;
using MediatR;

namespace EShop.API.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ResultT<ProductDto>>;
}
