using FudbalskiTurnir.DAL.Models; 
using Microsoft.AspNetCore.Identity;

namespace FudbalskiTurnir.DAL 
{
    public static class SeedData
    {
        public static async Task Initialize(
            IServiceProvider serviceProvider,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@djole.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var user = new User 
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "Admin123!");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}