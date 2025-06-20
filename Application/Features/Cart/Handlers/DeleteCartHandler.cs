using EShop.API.Features.Cart.Commands;
using EShop.API.Repository.IRepository;
using MediatR;

namespace EShop.API.Features.Cart.Handlers
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, bool>
    {
        private readonly ICartRepository _cartRepo;

        public DeleteCartHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<bool> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            return await _cartRepo.DeleteCartASync(request.Id);
        }
    }
}