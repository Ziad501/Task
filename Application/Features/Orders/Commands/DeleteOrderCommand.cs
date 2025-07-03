using Domain.Abstractions;
using MediatR;

namespace Application.Features.Orders.Commands
{
    public record DeleteOrderCommand(Guid Id) : IRequest<Result>;
}
