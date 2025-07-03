using Application.Dtos;
using Application.Features.Orders.Queries;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models.Orders;
using MediatR;

namespace Application.Features.Orders.Handlers
{
    public class GetOrdersQueryHandler(IQueryRepository<Domain.Models.Orders.Order> _order): IRequestHandler<GetOrdersQuery, ResultT<PagedList<OrderDto>>>
    {
        public async Task<ResultT<PagedList<OrderDto>>> Handle(GetOrdersQuery request,CancellationToken cancellationToken)
        {
            var query = _order.GetAll();

            var dtoQuery = query.Select(order => new OrderDto
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
                }).ToList()});

            var paged = await PagedList<OrderDto>.CreateAsync(dtoQuery, request.Page, request.PageSize);

            return ResultT<PagedList<OrderDto>>.Success(paged);
        }
    }
}