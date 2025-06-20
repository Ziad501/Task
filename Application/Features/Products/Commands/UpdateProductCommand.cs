using EShop.API.Dtos;
using MediatR;

namespace EShop.API.Features.Products.Commands
{
    public record UpdateProductCommand(Guid Id, ProductUpdateDto Dto) : IRequest;
}
