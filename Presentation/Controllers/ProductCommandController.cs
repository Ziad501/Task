using Application.Dtos;
using Application.Features.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "InventoryManager")]
    public class ProductCommandController(IMediator _mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> AddAsync([FromBody]ProductCreateDto dto)
        {
            var result = await _mediator.Send(new AddProductCommand(dto));
            if (result.IsFailure)
            {
                return BadRequest(result.Error.description);
            }
            return Ok(result.Value);
        }
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductUpdateDto dto)
        {
            var result =await _mediator.Send(new UpdateProductCommand(id, dto));

            if (result.Error.type == "NotFound")
            {
                return NotFound(result.Error.description);
            }
            if (result.Error.type == "IdMissMatch")
            {
                return BadRequest(result.Error.description);
            }
            if (result.IsFailure)
            {
                return BadRequest(result.Error.description);
            }
            return NoContent();
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result =await _mediator.Send(new DeleteProductCommand(id));
            if (result.Error.type == "NotFound")
            {
                return NotFound(result.Error.description);
            }
            if (result.IsFailure)
            {
                return BadRequest(result.Error.description);
            }
            return NoContent();
        }
    }
}
