using Application.Dtos;
using Application.Features.Cart.Commands;
using Application.Interfaces;
using Application.validators;
using Domain.Abstractions;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Features.Cart.Handlers
{
    public class UpdateCartHandler(ICartRepository _cartRepo, IValidator<CartDto> validator) : IRequestHandler<UpdateCartCommand, ResultT<CartDto>>
    {
        public async Task<ResultT<CartDto>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(request.CartDto, cancellationToken);
            if (!validation.IsValid)
                return new Error("validation_error", string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));
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
            return ResultT<CartDto>.Success(cartDto);
        }
    }
}