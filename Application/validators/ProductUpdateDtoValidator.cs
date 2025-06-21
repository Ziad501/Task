using EShop.API.Dtos;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EShop.API.validators
{
    public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
    {
        private readonly IQueryRepository<Product> _query;

        public ProductUpdateDtoValidator(IQueryRepository<Product> query)
        {
            _query =query;
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id is required.");
            
            RuleFor(x => x.Id)
           .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(150);

            RuleFor(x => x)
                .MustAsync(BeUniqName)
                .WithMessage("Product title exsists");


            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Text exceeded the limit");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(300);

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("CategoryId is required.");

            RuleForEach(x => x.Options).SetValidator(new ProductOptionUpdateDtoValidator());

        }
        public async Task<bool> BeUniqName(ProductUpdateDto dto, CancellationToken cancellationToken)
        {
            var exsits = await _query.GetAsync(p => p.Title == dto.Title && p.Id != dto.Id , cancellationToken: cancellationToken);
            return exsits == null;
        }
    }
}
