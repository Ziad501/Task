using Application.Dtos.CartDtos;
using FluentValidation;

namespace Application.validators.CartValidators
{
    public class CartDtoValidator : AbstractValidator<CartDto>
    {
        public CartDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new CartItemValidator());
        }
    }
}