using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Cart.Commands
{

    public record UpdateCartCommand(CartDto CartDto) : IRequest<ResultT<CartDto>>;
}
