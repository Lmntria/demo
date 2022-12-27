using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;
using System.Diagnostics;

namespace QuarterApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly QuarterDbContext _context;
		public HomeController(QuarterDbContext context)
		{
			_context=context;
		}

		public IActionResult Index()
		{
			HomeVM homeVM = new HomeVM
			{
				Sliders = _context.Sliders.OrderBy(X => X.Order).ToList(),
				Cities = _context.Cities.ToList(),
				Categories = _context.Categories.ToList(),
				Houses = _context.Houses.Include(x => x.HouseImages)
				.Include(x=>x.Manager)
				.Include(x => x.HouseAmenities)
				.ThenInclude(x => x.Amenity).ToList(),
				Services = _context.OurServices.ToList(),
				Amenities = _context.Amenities.Take(8).ToList(),
			};
			return View(homeVM);
		}

	}
}