using Application.Dtos.OrderDtos;
using FluentValidation;

namespace Application.validators.OrderValidators
{
    public class PaymentSummeryValidator : AbstractValidator<PaymentSummeryDto>
    {
        public PaymentSummeryValidator()
        {
            RuleFor(x => x.Last4).NotEmpty().WithMessage("Last 4 digits of the card number are required.");
            RuleFor(x => x.ExpMonth).InclusiveBetween(1, 12).WithMessage("Expiration month must be between 1 and 12.");
            RuleFor(x => x.ExpYear).InclusiveBetween(0, 99).WithMessage("Expiration year must be a valid two-digit year.");
        }
    }
}
