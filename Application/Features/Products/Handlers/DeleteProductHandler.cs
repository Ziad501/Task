using EShop.API.Features.Products.Commands;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using MediatR;

public class DeleteProductHandler(
    IQueryRepository<Product> _query,ICommandRepository<Product> _cmd): IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        
        var product = await _query.GetAsync(p => p.Id == request.Id, tracked: true, cancellationToken: cancellationToken);
        if (product is null)
            throw new KeyNotFoundException("Product not found");

        await _cmd.DeleteAsync(product);
    }
}
