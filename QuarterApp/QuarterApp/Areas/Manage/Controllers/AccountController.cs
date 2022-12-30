using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Models;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
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

        public IActionResult Index()
        {
            return View();
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
    }
}
