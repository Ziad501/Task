using Application.Dtos;
using MediatR;

namespace Application.Features.Cart.Queries
{
    
    public record GetCartByIdQuery(Guid Id) : IRequest<CartDto>;

}
