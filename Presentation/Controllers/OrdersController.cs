using Application.Dtos;
using Application.Features;
using Application.Interfaces;
using Domain.Models;
using Domain.Models.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(ICartRepository _cart,ICommandRepository<Order> _cmd, IQueryRepository<Order> _query,IQueryRepository<Product> _product) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] CreateOrderDto dto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (dto == null)
            {
                return BadRequest("Order data cannot be null");
            }
            var cart = await _cart.GetCartAsync(dto.CartId);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }
            var items = new List<OrderedItem>();
            foreach (var item in cart.Items)
            {
                var product = await _product.GetAsync(p => p.Id == item.ProductId);
                if (product == null)
                {
                    return NotFound($"Product with ID {item.ProductId} not found");
                }
                var orderedItem = new ProductOrdered
                {
                    ProductID = item.ProductId,
                    ProductName = item.ProductTitle,
                    ProductImage = item.ImageUrl,
                    ProductSize = string.IsNullOrEmpty(item.Size)? "standard" : item.Size,
                    ProductPrice = item.Price,
                };
                var orderedItemEntity = new OrderedItem
                {
                    ItemOrdered = orderedItem,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                items.Add(orderedItemEntity);
            }
            var order = new Order
            {
                OrderedItems = items,
                ShippingAddress = dto.ShippingAddress,
                SubTotal = items.Sum(i => i.Price * i.Quantity),
                PaymentSummery =dto.PaymentSummery,
                PaymentIntentId = cart.Id.ToString(),
                BuyerEmail = userEmail ?? string.Empty,
            };
            await _cmd.AddAsync(order);
            var orderDto = new OrderDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                SubTotal = order.SubTotal,
                PaymentIntentId = order.PaymentIntentId,
                ShippingAddress = order.ShippingAddress,
                PaymentSummery = order.PaymentSummery,
                OrderedItems = order.OrderedItems
            };
            return CreatedAtRoute("GetOrder", new { id =orderDto.Id} ,orderDto);
        }

        [HttpGet("{id:guid}",Name ="GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            var order = await _query.GetAsync(o => o.Id == id);
            if (order == null) return NotFound();
            var dto = new OrderDto
            {
                Id = order.Id,
                BuyerEmail = order.BuyerEmail,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                SubTotal = order.SubTotal,
                PaymentIntentId = order.PaymentIntentId?? string.Empty,
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
            return Ok(dto);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<OrderDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var orders =  _query.GetAll();
            var dtos = orders.Select(order => new OrderDto
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
            });
            return await PagedList<OrderDto>.CreateAsync(dtos, page , pageSize);
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _query.GetAsync(o => o.Id == id);
            if (order == null) return NotFound();

            await _cmd.DeleteAsync(order);
            return NoContent();
        }
    }
}