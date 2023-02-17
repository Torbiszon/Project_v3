using Microsoft.AspNetCore.Identity;

namespace Project_v3.Models
{
    public class AppUser : IdentityUser
    {
        public List<Films> films { get; set; }

    }
}
