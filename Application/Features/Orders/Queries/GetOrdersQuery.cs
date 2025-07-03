using Application.Dtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Orders.Queries
{
    public record GetOrdersQuery(int Page, int PageSize) : IRequest<ResultT<PagedList<OrderDto>>>;
}