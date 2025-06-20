using EShop.API.Dtos;
using EShop.API.Features.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "InventoryManager")]
    public class ProductCommandController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> AddAsync([FromBody]ProductCreateDto dto)
        {
            var result = await _mediator.Send(new AddProductCommand(dto));
            return CreatedAtRoute("GetById", new { Id = result.Id }, result);

        }
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductUpdateDto dto)
        {
            await _mediator.Send(new UpdateProductCommand(id, dto));
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }

    }
}
