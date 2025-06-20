using EShop.API.Dtos;
using EShop.API.Features.Cart.Queries;
using EShop.API.Repository.IRepository;
using MediatR;

namespace EShop.API.Features.Cart.Handlers
{
    public class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, CartDto>
    {
        private readonly ICartRepository _cartRepo;

        public GetCartByIdHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public async Task<CartDto> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepo.GetCartAsync(request.Id);

            return new CartDto
            {
                Id = request.Id,
                Items = cart?.Items.Select(p => new CartItemsDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductTitle,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl
                }).ToList() ?? new List<CartItemsDto>()
            };
        }
    }
}