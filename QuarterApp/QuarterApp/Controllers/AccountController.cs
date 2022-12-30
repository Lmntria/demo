using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;
using System.Runtime.Intrinsics.Arm;

namespace QuarterApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(QuarterDbContext context, UserManager<AppUser> manager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _manager = manager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginVM memberLogin, string redirectUrl)
        {
            AppUser user = await _manager.FindByNameAsync(memberLogin.Username);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or passsword is incorrect");
                return View();
            }


            var result = await _signInManager.PasswordSignInAsync(user, memberLogin.Password, false, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Too many failed attempt,try again in few minutes");
                return View();
            }

            var roles = await _manager.GetRolesAsync(user);

            if (!roles.Contains("Member"))
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

            return RedirectToAction("index", "home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterVM member)
        {
            if (member.IsCreateConsent == false)
            {
                ModelState.AddModelError("IsCreateConsent", "Check box must be selected");

                return View();
            }


            if (!ModelState.IsValid)
                return View();

            if (await _manager.FindByNameAsync(member.Username) != null)
            {
                ModelState.AddModelError("Username", "Username is already taken");
                return View(member);
            }
            else if (await _manager.FindByEmailAsync(member.Email) != null)
            {
                ModelState.AddModelError("Email", "Email is already taken");
                return View(member);
            }

            AppUser newUser = new AppUser
            {
                Email = member.Email,
                Fullname = member.Fullname,
                UserName = member.Username

            };

            var result = await _manager.CreateAsync(newUser, member.Password);


            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View();
            }

            await _manager.AddToRoleAsync(newUser, "Member");

            return RedirectToAction("login");
        }
        public IActionResult Profile()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Profile()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

    }
}
