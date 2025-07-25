﻿using Application.Dtos.ProductDtos;
using Application.Features;
using Application.Features.Products.Queries;
using Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductQueryController(IMediator _mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedList<ProductDto>>> GetAllProducts(string? SearchedItem,int page, int pageSize,CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(SearchedItem,page,pageSize), cancellationToken);
            return Ok(result);

        }
        [HttpGet("{id:guid}",Name = "GetById" )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
                return BadRequest("Id cannot be an empty GUID");

            var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);

            if (product.IsFailure)
            {
                if (product.Error == Errors.NotFound)
                {
                    return NotFound(product.Error);
                }
                return BadRequest(product.Error);
            }

            return Ok(product.Value);
        }
    }   
}
