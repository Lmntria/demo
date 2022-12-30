using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuarterApp.Areas.Manage.ViewModels;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    //[Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> manager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _manager = manager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM adminLogin, string redirectUrl)
        {
            AppUser user = await _manager.FindByNameAsync(adminLogin.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or passsword is incorrect");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(user, adminLogin.Password, false, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Too many failed attempt,try again in few minutes");
                return View();
            }

            var roles = await _manager.GetRolesAsync(user);

            if (!roles.Contains("SuperAdmin") && !roles.Contains("Admin") && !roles.Contains("Editor"))
            {
                ModelState.AddModelError("", "Username or passsword is incorrect");
                return View();
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or passsword is incorrect");
                return View();
            }

            if (redirectUrl != null)
                return Redirect(redirectUrl);

            return RedirectToAction("index", "Dashboard");
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1= new IdentityRole("Member");
        //    IdentityRole role2 = new IdentityRole("SuperAdmin");
        //    IdentityRole role3 = new IdentityRole("Admin");
        //    IdentityRole role4 = new IdentityRole("Editor");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    await _roleManager.CreateAsync(role4);


        //    return Ok();
        //}
        //public async Task<IActionResult> CreateAdmin()
        //{

        //    AppUser admin = new AppUser
        //    {
        //        UserName ="SuperAdmin",
        //        Fullname="Seymur Fahradov",
        //    };

        //    await _manager.CreateAsync(admin, "Salam123");

        //    await _manager.AddToRoleAsync(admin, "SuperAdmin");

        //    return Ok();
        //}
    }
}
