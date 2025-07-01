using Application.Dtos;
using FluentValidation;

namespace Application.validators
{
    public class ProductOptionUpdateDtoValidator : AbstractValidator<ProductOptionUpdateDto>
    {
        public ProductOptionUpdateDtoValidator()
        {

            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size is required.")
                .MaximumLength(50).WithMessage("Text exceeded the limit");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}
