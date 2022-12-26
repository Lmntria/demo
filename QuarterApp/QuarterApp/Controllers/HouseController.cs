using Microsoft.AspNetCore.Mvc;

namespace QuarterApp.Controllers
{
    public class HouseController : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }
    }
}
