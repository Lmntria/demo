using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly IWebHostEnvironment _web;

        public SliderController(QuarterDbContext context,IWebHostEnvironment web)
        {
            _context= context;
            _web = web;
        }
        public IActionResult Index()
        {
            var model = _context.Sliders.OrderBy(x => x.Order).ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
