using EShop.API.Dtos;
using MediatR;

namespace EShop.API.Features.Cart.Queries
{
    
    public record GetCartByIdQuery(Guid Id) : IRequest<CartDto>;

}
