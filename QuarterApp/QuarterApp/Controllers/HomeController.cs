using MessagePack.Internal;
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
			var bedrooms = _context.Houses.Sum(x => x.BedroomCount);
			var bathrooms = _context.Houses.Sum(x => x.BathroomCount);
            var rooms = _context.Houses.Sum(x => x.RoomCount);

			ViewBag.Cities = _context.Cities.ToList();
			ViewBag.Categories = _context.Categories.ToList();

            HomeVM homeVM = new HomeVM
			{
				Sliders = _context.Sliders.OrderBy(X => X.Order).ToList(),
				Cities = _context.Cities.ToList(),
				Categories = _context.Categories.ToList(),
				Houses = _context.Houses.Include(x => x.HouseImages)
				.Include(x => x.Manager)
				.Include(x => x.HouseAmenities)
				.ThenInclude(x => x.Amenity).ToList(),
				Services = _context.OurServices.ToList(),
				Amenities = _context.Amenities.ToList(),
				TotalArea =(int) Math.Ceiling(_context.Houses.Sum(x=>x.Area)),
				TotalRoom=bedrooms+bathrooms+rooms,
				AboutUs=_context.AboutUs.Take(1).ToList(),
			};
			return View(homeVM);
		}

	}
}