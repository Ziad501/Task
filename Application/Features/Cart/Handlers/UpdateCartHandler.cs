using EShop.API.Dtos;
using EShop.API.Features.Cart.Commands;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using MediatR;

namespace EShop.API.Features.Cart.Handlers
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, CartDto>
    {
        private readonly ICartRepository _cartRepo;

        public UpdateCartHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<CartDto> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = new Models.Cart
            {
                Id = request.CartDto.Id,
                Items = request.CartDto.Items.Select(p => new CartItem
                {
                    ProductId = p.ProductId,
                    ProductTitle = p.ProductName,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                }).ToList()
            };

            var updatedCart = await _cartRepo.SetCartAsync(cart);
            if (updatedCart == null)
                return null;
            return request.CartDto;
        }
    }
}
