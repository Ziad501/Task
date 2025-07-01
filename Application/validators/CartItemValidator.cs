using Application.Dtos;
using FluentValidation;

namespace Application.validators
{
    public class CartItemValidator : AbstractValidator<CartItemsDto>
    {
        public CartItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.ProductName).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.ImageUrl).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}