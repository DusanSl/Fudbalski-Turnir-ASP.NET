using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Kreiraj Admin rolu
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Kreiraj User rolu
        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }

        // Kreiraj Admin korisnika
        var adminEmail = "admin@football.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
        else
        {
            // Ako admin već postoji, osiguraj da ima Admin rolu
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}