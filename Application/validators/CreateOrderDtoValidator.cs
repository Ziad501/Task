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

            RuleFor(x => x.PaymentSummery).NotNull();
            RuleFor(x => x.PaymentSummery.Last4)
                .NotEmpty().LessThanOrEqualTo(4).WithMessage("Last4 must be 4 characters");
            RuleFor(x => x.PaymentSummery.ExpMonth)
                .NotEmpty().LessThanOrEqualTo(2).WithMessage("ExpMonth must be 2 characters");
            RuleFor(x => x.PaymentSummery.ExpYear)
                .NotEmpty().LessThanOrEqualTo(2).WithMessage("ExpYear must be 2 characters");
        }
    }
}