using Microsoft.AspNetCore.Identity;

namespace BlogApi.SeedData;

public class RoleSeed
{
    private readonly RoleManager<IdentityRole> _roleManager; // yeni roller oluşturmak veya var mı diye kontrol etmek için kullanılır.
    private readonly UserManager<IdentityUser> _userManager; // kullanıcı oluşturmak bulmak veya tollerine eklemek için kullanılır

    public RoleSeed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task SeedAsync() 
    {
        string[] roles = { "Admin", "User" }; // roller tanımlanıyor

        foreach (var role in roles) // rolleri tek tek kontrol edip veritabanında yoksa ekliyor
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = "admin@gmail.com"; // admin kullanıcısı aranıyor 
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var user = new IdentityUser { UserName = "admin@admin.com", Email = adminEmail }; // yeni kullanıcı oluşturuluyor
            await _userManager.CreateAsync(user, "admin"); // "admin şifresiyle kullanıcıyı veri tabanına ekler
            await _userManager.AddToRoleAsync(user, "Admin"); // kullanıcıya admin  rolü veriliyor



        }
    }
}