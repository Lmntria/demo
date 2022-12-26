using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
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
    }
}
