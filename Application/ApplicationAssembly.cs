using EShop.API.Features.Products.Commands;
using EShop.API.validators;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
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