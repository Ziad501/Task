using Microsoft.AspNetCore.Identity;

namespace EShop.API.Models
{
    public class AppUser : IdentityUser
    {
        public string Address { get; set; } = string.Empty;
    }
}
