using Domain.Abstractions;
using EShop.API.Dtos;
using MediatR;

namespace EShop.API.Features.Products.Commands
{
    public record AddProductCommand(ProductCreateDto Dto) : IRequest<ResultT<ProductCreateDto>>;

}
