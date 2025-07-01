using Application.Dtos;
using Application.Interfaces;
using Domain.Models;
using FluentValidation;

namespace Application.validators
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        private readonly IQueryRepository<Category> _query;

        public CategoryUpdateDtoValidator(IQueryRepository<Category> query)
        {
            _query = query;
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category Name length exceeded the limit");

            RuleFor(x=>x)
                .MustAsync(BeUniqueName).WithMessage("category name exists");
        }
        public async Task<bool> BeUniqueName(CategoryUpdateDto dto, CancellationToken cancellationToken)
        {
            var exists = await _query.GetAsync(p=>p.Name == dto.Name && p.Id != dto.Id,
                cancellationToken: cancellationToken);
            return exists == null;
        }

    }
}
