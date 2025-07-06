using Domain.Models;
using FluentValidation;
using Application.Interfaces;
using Application.Dtos.ProductDtos;

namespace Application.validators.ProductValidators
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        private readonly IQueryRepository<Product> _query;
        private readonly IQueryRepository<Category> _categoryQuery;
        public ProductCreateDtoValidator(IQueryRepository<Product> query, IQueryRepository<Category> categoryQuery) 
        {
            _query = query;
            _categoryQuery = categoryQuery;
            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Title is required.")
           .MaximumLength(150).WithMessage("Title length exceeded the limit")
           .MustAsync(BeUniqTitle) .WithMessage("Product title exsists");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description length exceeded the limit");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(300).WithMessage("Url length exceeded the limit");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.")
                .MustAsync(CategoryIdMissMatch).WithMessage("Incorrect category Id");

            RuleForEach(x => x.Options).SetValidator(new ProductOptionCreateDtoValidator());
        }
        public async Task<bool> BeUniqTitle(string title, CancellationToken cancellationToken)
        {
            var exsits = await _query.GetAsync(p => p.Title == title, cancellationToken: cancellationToken);
            return exsits == null;
        }
        public async Task<bool> CategoryIdMissMatch(Guid categoryId, CancellationToken cancellationToken)
        {
            var exsits = await _categoryQuery.GetAsync(c => c.Id == categoryId, cancellationToken: cancellationToken);
            return exsits != null;
        }
    }
}
