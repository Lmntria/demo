using Microsoft.AspNetCore.Mvc;
using QuarterApp.DAL;
using QuarterApp.Helpers;
using QuarterApp.Models;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SaleManagerController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SaleManagerController(QuarterDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model = _context.SaleManagers.ToList();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SaleManager manager)
        {

            if (manager.ImageFile != null && manager.ImageFile.ContentType != "image/png" && manager.ImageFile.ContentType != "image/jpeg" && manager.ImageFile.ContentType != "image/jpg")
            {
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg or image/jpg");
                return View();
            }

            if (manager.ImageFile != null && manager.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "ImageFile size must be lower than 3MB");
                return View();
            }


            if (!ModelState.IsValid)
                return View();
                

            manager.Image = FileManager.Save(manager.ImageFile, _env.WebRootPath, "uploads/saleManager");

            _context.SaleManagers.Add(manager);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var manager = _context.SaleManagers.FirstOrDefault(x => x.Id == id);


            if (manager == null)
                return RedirectToAction("error", "dashboard");

            return View(manager);
        }
        [HttpPost]
        public IActionResult Edit(SaleManager manager)
        {
            if (manager.ImageFile != null && manager.ImageFile.ContentType != "image/png" && manager.ImageFile.ContentType != "image/jpeg" && manager.ImageFile.ContentType != "image/jpg")
            {
                ModelState.AddModelError("ImageFile", "Content type must be image/png or image/jpeg or image/jpg");
                return View();
            }

            if (manager.ImageFile != null && manager.ImageFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "ImageFile size must be lower than 3MB");
                return View();
            }

            if (!ModelState.IsValid)
                return View();

            var existedManager =_context.SaleManagers.FirstOrDefault(x=>x.Id==manager.Id);

            if(existedManager==null)
                return RedirectToAction("error", "dashboard");


            if (existedManager.ImageFile != null)
            {
                var newImageName = FileManager.Save(manager.ImageFile, _env.WebRootPath, "uploads/saleManager");

                FileManager.Delete(_env.WebRootPath, "uploads/saleManager", existedManager.Image);

                existedManager.Image = newImageName;
            }

            existedManager.Fullname = manager.Fullname;

            existedManager.Description = manager.Description;


            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var manager = _context.SaleManagers.FirstOrDefault(x => x.Id==id);

            if (manager == null)
                return RedirectToAction("error", "dashboard");

            FileManager.Delete(_env.WebRootPath, "uploads/saleManager", manager.Image);

            _context.SaleManagers.Remove(manager);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
