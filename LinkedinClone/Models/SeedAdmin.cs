using Microsoft.AspNetCore.Identity;

namespace LinkedinClone.Models
{
    public class SeedAdmin: IHostedService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public SeedAdmin(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var adminUser = new User()
            {
                Email = "admin01@gmail.com",
                UserName = "Admin01_"
            };

            var save = await _userManager.CreateAsync(adminUser, "Admin01_");
            if (save.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin) && (!await _roleManager.RoleExistsAsync(UserRoles.User)))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
