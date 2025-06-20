using EShop.API.Dtos;
using MediatR;

namespace EShop.API.Features.Cart.Commands
{
    
    public record UpdateCartCommand(CartDto CartDto) : IRequest<CartDto>;
}
