using Application.Dtos;
using FluentValidation;

namespace Application.validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("CartId is required");

            RuleFor(x => x.ShippingAddress).NotNull();
            RuleFor(x => x.ShippingAddress.Name).NotEmpty();
            RuleFor(x => x.ShippingAddress.Line1).NotEmpty();
            RuleFor(x => x.ShippingAddress.City).NotEmpty();
            RuleFor(x => x.ShippingAddress.State).NotEmpty();
            RuleFor(x => x.ShippingAddress.PostalCode).NotEmpty();
            RuleFor(x => x.ShippingAddress.Country).NotEmpty();

            RuleFor(x => x.PaymentSummery.Last4).InclusiveBetween(0, 9999).WithMessage("Last4 must be a 4-digit number");

            RuleFor(x => x.PaymentSummery.ExpMonth).InclusiveBetween(1, 12).WithMessage("ExpMonth must be 1-12");

            RuleFor(x => x.PaymentSummery.ExpYear).InclusiveBetween(0, 99).WithMessage("ExpYear must be 0-99");
        }
    }
}