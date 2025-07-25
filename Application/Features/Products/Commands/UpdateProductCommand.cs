﻿using Application.Dtos.ProductDtos;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Products.Commands
{
    public record UpdateProductCommand(Guid Id, ProductUpdateDto Dto) : IRequest<ResultT<ProductDto>>;
}
