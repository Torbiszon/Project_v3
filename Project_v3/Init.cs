using Microsoft.AspNetCore.Identity;
using Project_v3.Models;

namespace Project_v3
{
    public class Init
    {
        public static async void Seed(IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope()) 
            { 
                var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>)); 
                if (await roleManager.RoleExistsAsync("User") == false) 
                {  
                    IdentityRole role = new IdentityRole("User"); await roleManager.CreateAsync(role); 
                } 
                if (await roleManager.RoleExistsAsync("Moderator") == false) 
                { 
                    IdentityRole role = new IdentityRole("Moderator"); await roleManager.CreateAsync(role); 
                }
                var userMenager = (UserManager<AppUser>)scope.ServiceProvider.GetService(typeof(UserManager<AppUser>));
                AppUser mod = await userMenager.FindByNameAsync("moderator");
                if (mod == null)
                {
                    var user = new AppUser
                    {
                        UserName = "moderator",
                        Email = "mod@mod.com",
                    };

                    await userMenager.CreateAsync(user, "Kacper123#");
                    await userMenager.AddToRoleAsync(user, "Moderator");
                }
            }
            
        }

    }
}
