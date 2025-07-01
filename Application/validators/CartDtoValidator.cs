using Application.Dtos;
using FluentValidation;

namespace Application.validators
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