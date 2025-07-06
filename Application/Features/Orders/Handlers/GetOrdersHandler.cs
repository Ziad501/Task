using Application.Dtos;
using Application.Dtos.OrderDtos;
using Application.Features.Orders.Queries;
using Application.Interfaces;
using Domain.Abstractions;
using Domain.Models.Orders;
using MediatR;

namespace Application.Features.Orders.Handlers
{
    public class GetOrdersQueryHandler(IQueryRepository<Order> _order) : IRequestHandler<GetOrdersQuery, ResultT<PagedList<OrderDto>>>
    {
        public async Task<ResultT<PagedList<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
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
                }).ToList() });

            var paged = await PagedList<OrderDto>.CreateAsync(dtoQuery, request.Page, request.PageSize, cancellationToken);
            return ResultT<PagedList<OrderDto>>.Success(paged);
        }
    }
}