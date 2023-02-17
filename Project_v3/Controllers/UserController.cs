using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_v3.Models;

namespace Project_v3.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly AplicationDbContext _context;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("register")]
        public IActionResult Register() 
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            AppUser user = new AppUser()
            {
                UserName = register.UserName,
                Email = register.Email,
            };
            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                var role = _roleManager.FindByNameAsync("User").Result;
                if(role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name); 
                    return RedirectToAction("login","user");
                }


            }
            return View(register);

        }
    }
}
