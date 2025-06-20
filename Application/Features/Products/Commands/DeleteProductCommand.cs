using MediatR;

namespace EShop.API.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest;

}
