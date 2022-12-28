using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;

namespace QuarterApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly QuarterDbContext _context;

        public ShopController(QuarterDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var house = _context.Houses
            .Include(x => x.City)
            .Include(x => x.HouseImages)
            .Include(x => x.HouseAmenities).ThenInclude(x => x.Amenity)
            .Include(x => x.Manager).ToList();

            ShopVm shopVm = new ShopVm
            {
                Houses = house
            };

            return View(shopVm);
        }
    }
}
