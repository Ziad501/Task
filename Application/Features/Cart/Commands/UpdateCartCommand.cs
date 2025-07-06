using Application.Dtos.CartDtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Cart.Commands
{

    public record UpdateCartCommand(CartDto CartDto) : IRequest<ResultT<CartDto>>;
}
