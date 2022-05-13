using Microsoft.AspNetCore.Identity;
using static Reservation_vols_version3_withIdentity_démo.Models.Enum;

namespace Reservation_vols_version3_withIdentity_démo.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));

        }

        public static async Task SeedSuperAdminAsync(UserManager<IdentityUser>userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "superadmin",
                Email = "superadmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Test123<3");
                    await userManager.AddToRoleAsync(defaultUser,
                    Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser,
                    Roles.Moderator.ToString());
                    await userManager.AddToRoleAsync(defaultUser,
                    Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser,
                    Roles.SuperAdmin.ToString());
                }
            }
        }
    }
}
