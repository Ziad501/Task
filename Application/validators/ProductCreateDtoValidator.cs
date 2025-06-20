using EShop.API.Dtos;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using FluentValidation;

namespace EShop.API.validators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        private readonly IQueryRepository<Product> _query;

        public ProductCreateDtoValidator(IQueryRepository<Product> query) 
        {
            _query = query;

            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required.")
           .MaximumLength(150).WithMessage("Title length exceeded the limit")
           .MustAsync(BeUniqTitle) .WithMessage("Product title exsists");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description length exceeded the limit");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(300).WithMessage("Url length exceeded the limit");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");

            RuleForEach(x => x.Options).SetValidator(new ProductOptionCreateDtoValidator());
        }
        public async Task<bool> BeUniqTitle(string title, CancellationToken cancellationToken)
        {
            var exsits = await _query.GetAsync(p => p.Title == title, cancellationToken: cancellationToken);
            return exsits == null;
        }
    }
}
