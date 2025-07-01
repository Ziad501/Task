using Application.Features.Products.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models;
using MediatR;

namespace Application.Features.Products.Handlers
{
    public class DeleteProductHandler(
    IQueryRepository<Product> _query, ICommandRepository<Product> _cmd) : IRequestHandler<DeleteProductCommand, Result>
    {
        public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            var product = await _query.GetAsync(p => p.Id == request.Id, cancellationToken: cancellationToken);
            if (product is null)
                return Errors.NotFound;

            await _cmd.DeleteAsync(product);
            return Result.Success();
        }
    }
}
