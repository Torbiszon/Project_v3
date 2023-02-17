using Microsoft.AspNetCore.Identity;

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
            }
        }

    }
}
