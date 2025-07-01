using Domain.Abstractions;
using MediatR;

namespace Application.Features.Cart.Commands
{
    public record DeleteCartCommand(Guid Id) : IRequest<Result>;

}
