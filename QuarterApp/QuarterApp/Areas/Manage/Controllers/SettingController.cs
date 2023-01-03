using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class SettingController : Controller
    {
        private readonly QuarterDbContext _context;
        public SettingController(QuarterDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Settings.ToList();

            return View(model);
        }
        public IActionResult Edit(string key)
        {
            var setting = _context.Settings.FirstOrDefault(x => x.Key == key);

            if (setting == null)
                return RedirectToAction("error", "dashboard");

            return View(setting);
        }
        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Settings.Any(x => x.Key == setting.Key && x.Value == setting.Value))
            {
                ModelState.AddModelError("Value", "This Value is already taken");
                return View();
            }

            var editedSetting = _context.Settings.FirstOrDefault(x => x.Key == setting.Key);

            if (editedSetting == null)
                return RedirectToAction("error", "dashboard");

            editedSetting.Value = setting.Value;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string key)
        {
            var setting = _context.Settings.FirstOrDefault(x => x.Key == key);

            if (setting == null)
                return RedirectToAction("error", "dashboard");


            _context.Settings.Remove(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
