using Application.Features.Products.Commands;
using Application.validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace Application
{
    public static class ApplicationAssembly
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddProductCommand).Assembly));
            services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();
            return services;
        }
    }
}