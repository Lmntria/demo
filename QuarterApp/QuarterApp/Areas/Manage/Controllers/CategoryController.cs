using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Models;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly QuarterDbContext _context;
        public CategoryController(QuarterDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.Categories.ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            if (_context.Categories.Any(X => X.Name == category.Name))
            {
                ModelState.AddModelError("Name", "This city name is already taken");
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
                return RedirectToAction("error", "dashboard");



            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (!ModelState.IsValid)
                return View();


            if (_context.Categories.Any(x => x.Id == category.Id && x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "This city name is already taken");
                return View();
            }

            var editedCategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if(editedCategory== null)
                return RedirectToAction("error", "dashboard");

            editedCategory.Name = category.Name;

            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if(category== null)
                return RedirectToAction("error", "dashboard");

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
