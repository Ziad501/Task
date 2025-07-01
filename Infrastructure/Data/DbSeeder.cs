using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndUsersAsync(IApplicationBuilder app)
        {
            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var roles = new[] { "SalesManagers", "Client", "InventoryManager" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var users = new[]
                {
                    new { Email = "InventoryManager@task.com", Password = "Pa$$w0rd", Role = "InventoryManager" },
                    new { Email = "SalesManagers@task.com", Password = "Pa$$w0rd", Role = "SalesManagers" },
                    new { Email = "Client@task.com", Password = "Pa$$w0rd", Role = "Client" }
                };

                foreach (var u in users)
                {
                    if (await userManager.FindByEmailAsync(u.Email) is null)
                    {
                        var user = new AppUser
                        {
                            UserName = u.Email,
                            Email = u.Email,
                            EmailConfirmed = true
                        };

                        await userManager.CreateAsync(user, u.Password);
                        await userManager.AddToRoleAsync(user, u.Role);
                    }
                }
            }
        }
    }
}