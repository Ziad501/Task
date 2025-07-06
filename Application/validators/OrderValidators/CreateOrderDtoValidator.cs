using Application.Dtos.OrderDtos;
using FluentValidation;

namespace Application.validators.OrderValidators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("CartId is required");


            RuleFor(x => x.ShippingAddress)
                .NotNull().WithMessage("Shipping address is required.")
                .SetValidator(new ShippingAddressValidator()); 

            RuleFor(x => x.PaymentSummery)
                .NotNull().WithMessage("Payment summery is required.")
                .SetValidator(new PaymentSummeryValidator());
        }
    }
}
      