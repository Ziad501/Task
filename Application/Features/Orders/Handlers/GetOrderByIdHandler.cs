using Application.Dtos;
using Application.Features.Orders.Queries;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models.Orders;
using MediatR;

namespace Application.Features.Orders.Handlers
{
    public class GetOrderByIdQueryHandler(IQueryRepository<Domain.Models.Orders.Order> _order): IRequestHandler<GetOrderByIdQuery, ResultT<OrderDto>>
    {
        public async Task<ResultT<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _order.GetAsync(o => o.Id == request.Id);
            if (order == null)
                return Errors.NotFound;

            var dto = new OrderDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                SubTotal = order.SubTotal,
                PaymentIntentId = order.PaymentIntentId ?? string.Empty,
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
            return ResultT<OrderDto>.Success(dto);
        }
    }
}
