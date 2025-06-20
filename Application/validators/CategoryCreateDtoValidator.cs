using EShop.API.Dtos;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using FluentValidation;

namespace EShop.API.validators
{

    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        private readonly IQueryRepository<Category> _query;

        public CategoryCreateDtoValidator(IQueryRepository<Category> query)
        {
            _query = query;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Text exceeded the limit")
                .MustAsync(BeUniqueName).WithMessage("category name exists");
        }
        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            var exists = await _query.GetAsync(p => p.Name == name, cancellationToken: cancellationToken);
            return exists == null;
        }
    }
}
