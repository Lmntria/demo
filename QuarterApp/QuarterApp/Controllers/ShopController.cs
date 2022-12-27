using Microsoft.AspNetCore.Mvc;

namespace QuarterApp.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
