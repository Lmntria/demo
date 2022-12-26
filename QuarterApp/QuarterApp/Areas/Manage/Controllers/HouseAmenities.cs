using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterApp.DAL;
using QuarterApp.Models;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    public class HouseAmenities : Controller
    {
        private readonly QuarterDbContext _context;

        public HouseAmenities(QuarterDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.HouseAmenities.Include(x => x.Amenity).Include(x => x.House).ToList();

            ViewBag.Amenity = _context.Amenities.ToList();
            ViewBag.House = _context.Houses.ToList();

            return View(model);
        }
        public IActionResult Create()
        {

            ViewBag.Amenity = _context.Amenities.ToList();
            ViewBag.House = _context.Houses.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(HouseAmenity houseAmenities)
        {
            if (!_context.Amenities.Any(x => x.Id == houseAmenities.AmenityId))
                ModelState.AddModelError("AmenityId", "Amenity not found");
            if (!_context.Houses.Any(x => x.Id == houseAmenities.HouseId))
                ModelState.AddModelError("HouseId", "House not found");

            if (!ModelState.IsValid)
            {
                ViewBag.Amenity = _context.Amenities.ToList();
                ViewBag.House = _context.Houses.ToList();

                return View();
            }

            _context.HouseAmenities.Add(houseAmenities);
            _context.SaveChanges();


            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Amenity = _context.Amenities.ToList();
            ViewBag.House = _context.Houses.ToList();

            var houseAmenity = _context.HouseAmenities.FirstOrDefault(x => x.Id == id);
            if (houseAmenity == null)
                return RedirectToAction("error", "dashboard");


            return View(houseAmenity);
        }
        [HttpPost]
        public IActionResult Edit(HouseAmenity houseAmenities)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Amenity = _context.Amenities.ToList();
                ViewBag.House = _context.Houses.ToList();
                return View();
            }


            if (_context.HouseAmenities.Any(x => x.Id == houseAmenities.Id && x.AmenityId == houseAmenities.AmenityId))
            {
                ModelState.AddModelError("Name", "This amenity name is already taken");
                return View();
            }

            var editedHouseAmenity = _context.HouseAmenities.FirstOrDefault(x => x.Id == houseAmenities.Id);

            if(editedHouseAmenity==null)
                return RedirectToAction("error", "dashboard");


            editedHouseAmenity.HouseId = houseAmenities.HouseId;
            editedHouseAmenity.AmenityId = houseAmenities.AmenityId;

            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var houseAmenity = _context.HouseAmenities.FirstOrDefault(x => x.Id == id);

            if(houseAmenity==null)
                return RedirectToAction("error", "dashboard");

            _context.HouseAmenities.Remove(houseAmenity);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
