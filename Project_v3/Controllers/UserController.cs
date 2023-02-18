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
        public ActionResult Register() 
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm]RegisterModel register)
        {
            if (ModelState.IsValid)
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
                    if (role != null)
                    {
                        IdentityResult res = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            
            return View(register);

        }
        [AllowAnonymous]
        [HttpGet("login")]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel login)
        {
            var user = _context
                .Users
                .FirstOrDefault(x => x.UserName == login.UserName);
            if (user != null)
            {
                if ((await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false)).Succeeded)
                {
                    return RedirectToAction("index", "Films");
                }
            }
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
