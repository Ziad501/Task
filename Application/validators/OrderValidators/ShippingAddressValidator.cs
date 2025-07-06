using Application.Dtos.OrderDtos;
using FluentValidation;

namespace Application.validators.OrderValidators
{
    public class ShippingAddressValidator : AbstractValidator<ShippingAddressDto>
    {
        public ShippingAddressValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Line1).NotEmpty().WithMessage("Address Line 1 is required.");
            RuleFor(x => x.City).NotEmpty().WithMessage("City is required.");
            RuleFor(x => x.State).NotEmpty().WithMessage("State is required.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("Postal Code is required.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Country is required.");
        }
    }
}
