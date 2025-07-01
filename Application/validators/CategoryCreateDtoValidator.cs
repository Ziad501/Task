using Application.Dtos;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;

namespace Application.validators
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
