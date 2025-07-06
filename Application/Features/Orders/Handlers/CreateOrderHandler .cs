using Application.Dtos.OrderDtos;
using Application.Features.Orders.Commands;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models;
using Domain.Models.Orders;
using FluentValidation;
using MediatR;

namespace Application.Features.Orders.Handlers
{
    public class CreateOrderHandler(ICartRepository _cart, ICommandRepository<Order> _order, IQueryRepository<Product> _product, IValidator<CreateOrderDto> validator) : IRequestHandler<CreateOrderCommand, ResultT<OrderDto>>
    {
        public async Task<ResultT<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(request.Dto, cancellationToken);
            if (!validation.IsValid)
                return new Error("validation_error", string.Join(", ", validation.Errors.Select(e => e.ErrorMessage)));

            var dto = request.Dto;
            var buyerEmail = request.BuyerEmail;

            var cart = await _cart.GetCartAsync(dto.CartId);
            if (cart == null)
                return Errors.NoSuchCart;

            var items = new List<OrderedItem>();
            foreach (var item in cart.Items)
            {
                var product = await _product.GetAsync(p => p.Id == item.ProductId,cancellationToken:cancellationToken);
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

            var order = new Order
            {
                OrderedItems = items,
                ShippingAddress = new ShippingAddress
                {
                    Name = dto.ShippingAddress.Name,
                    Line1 = dto.ShippingAddress.Line1,
                    Line2 = dto.ShippingAddress.Line2,
                    City = dto.ShippingAddress.City,
                    State = dto.ShippingAddress.State,
                    PostalCode = dto.ShippingAddress.PostalCode,
                    Country = dto.ShippingAddress.Country
                },
                SubTotal = items.Sum(i => i.Price * i.Quantity),
                PaymentSummery = new PaymentSummery
                {
                    Last4 = dto.PaymentSummery.Last4,
                    ExpMonth = dto.PaymentSummery.ExpMonth,
                    ExpYear = dto.PaymentSummery.ExpYear
                },
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
                ShippingAddress = new ShippingAddressDto
                {
                    Name = order.ShippingAddress.Name,
                    Line1 = order.ShippingAddress.Line1,
                    Line2 = order.ShippingAddress.Line2,
                    City = order.ShippingAddress.City,
                    State = order.ShippingAddress.State,
                    PostalCode = order.ShippingAddress.PostalCode,
                    Country = order.ShippingAddress.Country
                },
                PaymentSummery = new PaymentSummeryDto
                {
                    Last4 = order.PaymentSummery.Last4,
                    ExpMonth = order.PaymentSummery.ExpMonth,
                    ExpYear = order.PaymentSummery.ExpYear
                },
                OrderedItems = order.OrderedItems.Select(oi => new OrderedItemDto
                {
                    ProductID = oi.ItemOrdered.ProductID,
                    ProductName = oi.ItemOrdered.ProductName,
                    ProductImage = oi.ItemOrdered.ProductImage,
                    ProductSize = oi.ItemOrdered.ProductSize,
                    ProductPrice = oi.ItemOrdered.ProductPrice,
                    Price = oi.Price,
                    Quantity = oi.Quantity
                }).ToList()
            };

            return ResultT<OrderDto>.Success(resultDto);
        }
    }
}
