using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure
{
    public static class InfrastructureAssembly
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddScoped<ICartRepository, CartRepository>();

            services.AddSingleton<IConnectionMultiplexer>(_ =>
            {
                var redisConfig = new ConfigurationOptions
                {
                    EndPoints = { "closing-gannet-15798.upstash.io:6379" },
                    User = "default",
                    Password = "AT22AAIjcDEyZjIwYzJmY2UxMWQ0YWVlYTY3NDNmNjQ1YmU5ZTdjMnAxMA",
                    Ssl = true,
                    AbortOnConnectFail = false,
                    ConnectTimeout = 10000,
                    ConnectRetry = 3
                };
                return ConnectionMultiplexer.Connect(redisConfig);
            });

            return services;
        }
    }
}