using Application.Dtos;
using Application.Features.Orders.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models;
using Domain.Models.Orders;
using MediatR;

namespace Application.Features.Order.Handlers
{
    public class CreateOrderHandler(ICartRepository _cart, ICommandRepository<Domain.Models.Orders.Order> _order, IQueryRepository<Product> _product) : IRequestHandler<CreateOrderCommand, ResultT<OrderDto>>
    {
        public async Task<ResultT<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var buyerEmail = request.BuyerEmail;

            var cart = await _cart.GetCartAsync(dto.CartId);
            if (cart == null)
                return Errors.NoSuchCart;

            var items = new List<OrderedItem>();
            foreach (var item in cart.Items)
            {
                var product = await _product.GetAsync(p => p.Id == item.ProductId);
                if (product == null)
                    return Errors.NotFound;

                items.Add(new OrderedItem
                {
                    ItemOrdered = new ProductOrdered
                    {
                        ProductID = item.ProductId,
                        ProductName = item.ProductTitle,
                        ProductImage = item.ImageUrl,
                        ProductSize = string.IsNullOrEmpty(item.Size) ? "standard" : item.Size,
                        ProductPrice = item.Price
                    },
                    Price = item.Price,
                    Quantity = item.Quantity
                });
            }

            var order = new Domain.Models.Orders.Order
            {
                OrderedItems = items,
                ShippingAddress = dto.ShippingAddress,
                SubTotal = items.Sum(i => i.Price * i.Quantity),
                PaymentSummery = dto.PaymentSummery,
                PaymentIntentId = dto.CartId.ToString(),
                BuyerEmail = buyerEmail ?? string.Empty,
            };

            await _order.AddAsync(order);

            var resultDto = new OrderDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                SubTotal = order.SubTotal,
                PaymentIntentId = order.PaymentIntentId,
                ShippingAddress = order.ShippingAddress,
                PaymentSummery = order.PaymentSummery,
                OrderedItems = order.OrderedItems.Select(oi => new OrderedItem
                {
                    ItemOrdered = new ProductOrdered
                    {
                        ProductID = oi.ItemOrdered.ProductID,
                        ProductName = oi.ItemOrdered.ProductName,
                        ProductImage = oi.ItemOrdered.ProductImage,
                        ProductSize = oi.ItemOrdered.ProductSize,
                        ProductPrice = oi.ItemOrdered.ProductPrice
                    },
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            };
            return ResultT<OrderDto>.Success(resultDto);
        }
    }
}