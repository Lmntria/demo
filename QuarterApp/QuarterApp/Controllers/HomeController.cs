using Microsoft.AspNetCore.Mvc;
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
				Sliders =_context.Sliders.OrderBy(X=>X.Order).ToList(),
			};
			return View(homeVM);
		}

	}
}