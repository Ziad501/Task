﻿using Application.Dtos.CartDtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Cart.Queries
{
    public record GetCartByIdQuery(Guid Id) : IRequest<ResultT<CartDto>>;

}
