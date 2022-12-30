using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Helpers;
using QuarterApp.Models;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]

    public class OurServiceController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly IWebHostEnvironment _env;

        public OurServiceController(QuarterDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var model = _context.OurServices.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OurService service)
        {

            if (service.ImageFile != null && service.ImageFile.ContentType != "image/png" && service.ImageFile.ContentType != "image/jpeg" && service.ImageFile.ContentType != "image/jpg")
            {
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg or image/jpg");
                return View();
            }

            if (service.ImageFile != null && service.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "ImageFile size must be lower than 3MB");
                return View();
            }


            if (!ModelState.IsValid)
                return View();


            service.Image = FileManager.Save(service.ImageFile, _env.WebRootPath, "uploads/ourServices");

            _context.OurServices.Add(service);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var service = _context.OurServices.FirstOrDefault(x => x.Id == id);

            if (service == null)
                return RedirectToAction("error", "dashboard");


            return View(service);  
        }
        [HttpPost]
        public IActionResult Edit(OurService service)
        {
            if (service.ImageFile != null && service.ImageFile.ContentType != "image/png" && service.ImageFile.ContentType != "image/jpeg" && service.ImageFile.ContentType != "image/jpg")
            {
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg or image/jpg");
                return View();
            }

            if (service.ImageFile != null && service.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "ImageFile size must be lower than 3MB");
                return View();
            }

            if (!ModelState.IsValid)
                return View();

            var existedService = _context.OurServices.FirstOrDefault(x => x.Id == service.Id);

            if (existedService == null)
                return RedirectToAction("error", "dashboard");


            if (existedService.ImageFile != null)
            {
                var newImageName = FileManager.Save(service.ImageFile, _env.WebRootPath, "uploads/ourServices");

                FileManager.Delete(_env.WebRootPath, "uploads/ourServices", existedService.Image);

                existedService.Image = newImageName;
            }

            existedService.Name = service.Name;

            existedService.Description = service.Description;


            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var service = _context.OurServices.FirstOrDefault(x => x.Id == id);

            if (service == null)
                return RedirectToAction("error", "dashboard");

            FileManager.Delete(_env.WebRootPath, "uploads/ourServices", service.Image);

            _context.OurServices.Remove(service);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
