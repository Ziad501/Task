using Application.Dtos;
using Domain.Abstractions.IRepository;
using Domain.Models;
using Domain.Models.Order;
using Domain.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(ICartRepository _cart,ICommandRepository<Order> _cmd, IQueryRepository<Product> _product) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Order>> AddAsync([FromBody] CreateOrderDto dto)
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
            return Ok(order);
        }

    }
}
