using Application.Dtos;
using Application.Features.Cart.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Features.Cart.Handlers
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, ResultT<CartDto>>
    {
        private readonly ICartRepository _cartRepo;

        public UpdateCartHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<ResultT<CartDto>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Domain.Models.Cart
            {
                Id = request.CartDto.Id,
                Items = request.CartDto.Items.Select(p => new CartItem
                {
                    ProductId = p.ProductId,
                    ProductTitle = p.ProductName,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };

            var updated = await _cartRepo.SetCartAsync(cart);

            if (updated is null)
                return Errors.CartUpdateFailed;
            var cartDto = new CartDto
            {
                Id = updated.Id,
                Items = updated.Items.Select(p => new CartItemsDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductTitle,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Quantity = p.Quantity
                }).ToList()
            };

            return ResultT<CartDto>.Success(request.CartDto);
        }
    }
}