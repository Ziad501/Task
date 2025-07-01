using Application.Features.Cart.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using MediatR;

namespace EShop.API.Features.Cart.Handlers
{
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, Result>
    {
        private readonly ICartRepository _cartRepo;

        public DeleteCartHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<Result> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _cartRepo.DeleteCartASync(request.Id);

            return deleted
                ? Result.Success()
                : Errors.CartDeleteFailed;
        }
    }
}