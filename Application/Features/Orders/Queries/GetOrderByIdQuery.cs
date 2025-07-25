﻿using Application.Dtos.OrderDtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public record GetOrderByIdQuery(Guid Id) : IRequest<ResultT<OrderDto>>;

}
