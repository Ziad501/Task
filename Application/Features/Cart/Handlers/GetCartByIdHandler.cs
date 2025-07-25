﻿using Application.Dtos.CartDtos;
using Application.Features.Cart.Queries;
using Application.Interfaces;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Cart.Handlers
{
    public class GetCartByIdHandler(ICartRepository _cartRepo) : IRequestHandler<GetCartByIdQuery, ResultT<CartDto>>
    {
        public async Task<ResultT<CartDto>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepo.GetCartAsync(request.Id);
            if (cart == null) return Errors.NoSuchCart;
            var cartDto = new CartDto
            {
                Id = request.Id,
                Items = cart?.Items.Select(p => new CartItemsDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductTitle,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                }).ToList() ?? new List<CartItemsDto>()
            };
            return ResultT<CartDto>.Success(cartDto);
        }
    }
}