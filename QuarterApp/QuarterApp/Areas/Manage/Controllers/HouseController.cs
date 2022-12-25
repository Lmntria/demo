using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuarterApp.DAL;
using QuarterApp.Helpers;
using QuarterApp.Models;

namespace QuarterApp.Areas.Manage.Controllers
{
    [Area("manage")]
    public class HouseController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly IWebHostEnvironment _env;

        public HouseController(QuarterDbContext context,IWebHostEnvironment env)
        {
            _context= context;
            _env = env;
        }
        public IActionResult Index()
        {
            var model=_context.Houses
                .Include(x=>x.HouseImages)
                .Include(x=>x.City)
                .Include(x=>x.Category)
                .Include(x=>x.Manager)
                .ToList();



            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.City = _context.Cities.ToList();
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Manager = _context.SaleManagers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(House house)
        {
            if(!_context.Cities.Any(x=>x.Id==house.CityId))
                ModelState.AddModelError("CityId", "City not found");
            if (!_context.Categories.Any(x => x.Id == house.CategoryId))
                ModelState.AddModelError("CategoryId", "Category not found");
            if (!_context.SaleManagers.Any(x => x.Id == house.ManagerId))
                ModelState.AddModelError("ManagerId", "Manager not found");


            _checkImageFiles(house.PosterImage, house.ImageFiles);

            if (!ModelState.IsValid)
            {
                ViewBag.City = _context.Cities.ToList();
                ViewBag.Category = _context.Categories.ToList();
                ViewBag.Manager = _context.SaleManagers.ToList();
                return View();
            }

            house.HouseImages = new List<HouseImage>();

            HouseImage poster = new HouseImage
            {
                Name = FileManager.Save(house.PosterImage, _env.WebRootPath, "uploads/houses"),
                PosterStatus = true
            };

            house.HouseImages.Add(poster);


            foreach (var imgFile in house.ImageFiles)
            {
                HouseImage houseImage = new HouseImage
                {
                    Name = FileManager.Save(house.PosterImage, _env.WebRootPath, "uploads/houses"),
                    PosterStatus = false
                };
                house.HouseImages.Add(houseImage);

            }

            _context.Houses.Add(house);
            _context.SaveChanges();

            return Ok(house);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(House house)
        {
            return View();
        }
        public IActionResult Delete(int id)
        {
            return View();
        }


















        private void _checkImageFiles(IFormFile poster,List<IFormFile> images)
        {
            if (poster == null)
                ModelState.AddModelError("PosterImage", "PosterImage is required");
            else if(poster.ContentType != "image/png" && poster.ContentType != "image/jpeg" && poster.ContentType!="image/jpg")
                ModelState.AddModelError("PosterImage", "ContentType is not image/png or image/jpeg or image/jpg");
            else if(poster.Length > 3145728)
                ModelState.AddModelError("PosterImage", "PosterImage size must be lower than 3MB");


            if (images != null)
            {
                foreach (var imgFile in images)
                {
                    if (imgFile.ContentType != "image/png" && imgFile.ContentType != "image/jpeg" && imgFile.ContentType != "image/jpg")
                        ModelState.AddModelError("ImageFiles", "ContentType is not image/png or image/jpeg or image/jpg");
                    else if (imgFile.Length > 3145728)
                        ModelState.AddModelError("ImageFiles", "ImageFiles size must be lower than 3MB");
                }
            }

        }
    }
}
