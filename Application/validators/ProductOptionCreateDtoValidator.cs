using EShop.API.Dtos;
using FluentValidation;

namespace EShop.API.validators
{
    public class ProductOptionCreateDtoValidator : AbstractValidator<ProductOptionCreateDto>
    {
        public ProductOptionCreateDtoValidator()
        {
            RuleFor(x => x.Size)
                .NotEmpty().WithMessage("Size is required.")
                .MaximumLength(50).WithMessage("Text exceeded the limit");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}
