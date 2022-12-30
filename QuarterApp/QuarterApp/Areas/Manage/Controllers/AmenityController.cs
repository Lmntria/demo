using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Models;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class AmenityController : Controller
    {
        private readonly QuarterDbContext _context;

        public AmenityController(QuarterDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model=_context.Amenities.ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Amenity amenity) 
        {
            if (!ModelState.IsValid)
                return View();

            if (_context.Amenities.Any(x => x.Name == amenity.Name))
            {
                ModelState.AddModelError("Name", "This amenity name is already taken");
                return View();
            }

            _context.Amenities.Add(amenity);
            _context.SaveChanges();

            return RedirectToAction("Index");


        }
        public IActionResult Edit(int id)
        {
            var amenity = _context.Amenities.FirstOrDefault(x => x.Id == id);
            if (amenity == null)
                return RedirectToAction("error", "dashboard");

            return View(amenity);

        }
        [HttpPost]
        public IActionResult Edit(Amenity amenity)
        {
            if (!ModelState.IsValid)
                return View();
            if (_context.Amenities.Any(x => x.Id == amenity.Id && x.Name == amenity.Name))
            {
                ModelState.AddModelError("Name", "This amenity name is already taken");
                return View();
            }

            var editedAmenity = _context.Amenities.FirstOrDefault(x => x.Id == amenity.Id);

            if (editedAmenity == null)
                return RedirectToAction("error", "dashboard");

            editedAmenity.Name = amenity.Name;
            editedAmenity.Icon = amenity.Icon;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var amenity = _context.Amenities.FirstOrDefault(x => x.Id == id);
            if (amenity == null)
                return RedirectToAction("error", "dashboard");

            _context.Amenities.Remove(amenity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
