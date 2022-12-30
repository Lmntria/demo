using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Models;
using System.Data;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class CityController : Controller
    {
        private readonly QuarterDbContext _context;

        public CityController(QuarterDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = _context.Cities.ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(City city)
        {
            if (!ModelState.IsValid)
                return View();

            if (_context.Cities.Any(x => x.Name == city.Name))
            {
                ModelState.AddModelError("Name", "This city name is already taken");
                return View();
            }

            _context.Cities.Add(city);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var city = _context.Cities.FirstOrDefault(x => x.Id == id);
            if (city==null)
                return RedirectToAction("error", "dashboard");

            return View(city);
        }
        [HttpPost]
        public IActionResult Edit(City city)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Cities.Any(x=>x.Id == city.Id && x.Name == city.Name))
            {
                ModelState.AddModelError("Name", "This city name is already taken");
                return View();
            }

            var editedCity = _context.Cities.FirstOrDefault(x => x.Id == city.Id);

            if (editedCity==null)
                return RedirectToAction("error", "dashboard");

            editedCity.Name = city.Name;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var city = _context.Cities.FirstOrDefault(x => x.Id == id);
            if (city == null)
                return RedirectToAction("error", "dashboard");

            _context.Cities.Remove(city);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
