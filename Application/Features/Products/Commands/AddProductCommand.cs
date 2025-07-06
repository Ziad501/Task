using Application.Dtos.ProductDtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record AddProductCommand(ProductCreateDto Dto) : IRequest<ResultT<ProductCreateDto>>;
}
