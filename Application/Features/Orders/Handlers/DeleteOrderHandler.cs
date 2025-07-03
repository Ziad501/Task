using Application.Features.Orders.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Orders.Handlers
{
    public class DeleteOrderCommandHandler(ICommandRepository<Domain.Models.Orders.Order> _cmd , IQueryRepository<Domain.Models.Orders.Order> _query) : IRequestHandler<DeleteOrderCommand, Result>
    {
        public async Task<Result> Handle(
            DeleteOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _query.GetAsync(o => o.Id == request.Id);
            if (order is null)
                return Errors.NotFound;

            await _cmd.DeleteAsync(order);
            return Result.Success();
        }
    }
}
