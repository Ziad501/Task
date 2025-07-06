using Application.Dtos.ProductDtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Products.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ResultT<ProductDto>>;
}
