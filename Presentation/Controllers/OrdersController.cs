using Application.Dtos;
using Application.Features.Orders.Commands;
using Application.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddAsync([FromBody] CreateOrderDto dto,CancellationToken cancellationToken)
        {
            if (dto is null)
                return BadRequest("Order data cannot be null");

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

            var result = await _mediator.Send(
                new CreateOrderCommand(dto, buyerEmail),
                cancellationToken
            );

            if (!result.IsSuccess)
                return BadRequest(result.Error.description);

            return CreatedAtRoute("GetOrder",new { id = result.Value.Id },result?.Value);
        }

        [HttpGet("{id:guid}", Name = "GetOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id),cancellationToken);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error.description);
            }

            return Ok(result.Value);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1,[FromQuery] int pageSize = 10,CancellationToken cancellationToken=default)
        {
            var result = await _mediator.Send(new GetOrdersQuery(page, pageSize),cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error.description);

            return Ok(result.Value);
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteOrderCommand(id), cancellationToken);
            if (!result.IsSuccess)
                return NotFound(result.Error.description);

            return NoContent();
        }

    }
}