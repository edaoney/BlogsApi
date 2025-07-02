using Microsoft.AspNetCore.Identity;

namespace BlogApi.SeedData;

public class RoleSeed
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public RoleSeed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedAsync()
    {
        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = "admin@gmail.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var user = new IdentityUser { UserName = "admin@admin.com", Email = adminEmail };
            await _userManager.CreateAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "Admin");



        }
    }
}