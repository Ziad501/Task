﻿using Application.Dtos.OrderDtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record CreateOrderCommand(CreateOrderDto Dto, string BuyerEmail) : IRequest<ResultT<OrderDto>>;

}
