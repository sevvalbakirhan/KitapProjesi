using Microsoft.AspNetCore.Identity;

namespace BookStore.MVC.Data
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            
            var roles = new[] { "Admin", "User" };
            foreach (var r in roles)
            {
                if (!await roleMgr.RoleExistsAsync(r))
                    await roleMgr.CreateAsync(new IdentityRole(r));
            }

          
            var adminEmail = "admin@bookstore.local";
            var admin = await userMgr.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var create = await userMgr.CreateAsync(admin, "Admin123!");
                if (!create.Succeeded)
                    throw new Exception("Admin oluşturulamadı: " +
                        string.Join(" | ", create.Errors.Select(e => $"{e.Code}:{e.Description}")));
            }

          
            if (!await userMgr.IsInRoleAsync(admin, "Admin"))
                await userMgr.AddToRoleAsync(admin, "Admin");
        }
    }
}