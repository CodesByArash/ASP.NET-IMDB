using api.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Helpers;

public static class Utils
{   
    public async static Task SeedAdminUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var adminEmail = "admin@example.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new AppUser
            {
                UserName = "admin",
                Email = adminEmail
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin@123");

            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}