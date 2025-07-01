using Presentation.Controllers;

namespace EShop.API
{
    public static class StartupControllers
    {
        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddApplicationPart(typeof(CartController).Assembly)
                    .AddControllersAsServices();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMe", policy =>
                {
                    policy.WithOrigins("https://localhost:7130", "https://localhost:7014")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            return services;
        }
    }
}
