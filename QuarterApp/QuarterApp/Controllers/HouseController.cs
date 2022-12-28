using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;

namespace QuarterApp.Controllers
{
    public class HouseController : Controller
    {
        private readonly QuarterDbContext _context;

        public HouseController(QuarterDbContext context)
        {
            _context= context;
        }
        public IActionResult Detail(int id)
        {

            House house = _context.Houses
                .Include(x=>x.City)
                .Include(x => x.HouseImages)
                .Include(x => x.HouseAmenities).ThenInclude(x => x.Amenity)
                .Include(x => x.Manager)
                .FirstOrDefault(x => x.Id == id);

            if (house == null)
                return RedirectToAction("error", "dashboard");

            HouseDetailVM detailVM = new HouseDetailVM
            {
                House = house,
                Amenities=_context.Amenities.ToList(),
                Houses=_context.Houses.ToList(),
            };


            return View(detailVM);
        }
    }
}
