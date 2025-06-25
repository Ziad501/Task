using Domain.Abstractions;
using EShop.API.Features.Products.Commands;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using MediatR;

public class DeleteProductHandler(
    IQueryRepository<Product> _query,ICommandRepository<Product> _cmd): IRequestHandler<DeleteProductCommand, Result>
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
