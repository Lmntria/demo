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
            ViewBag.Amenities = _context.Amenities.ToList();
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
                ViewBag.Amenities = _context.Amenities.ToList();

                return View();
            }

            house.HouseAmenities = new List<HouseAmenity>();

            foreach (var amenityId in house.AmenityIds)
            {
                if (!_context.Amenities.Any(x => x.Id == amenityId))
                {
                    ViewBag.City = _context.Cities.ToList();
                    ViewBag.Category = _context.Categories.ToList();
                    ViewBag.Manager = _context.SaleManagers.ToList();
                    ViewBag.Amenities = _context.Amenities.ToList();

                    ModelState.AddModelError("AmenityIds", "Amenity does not exists.");
                    return View();
                }

                HouseAmenity houseAmenity = new HouseAmenity
                {
                    AmenityId = amenityId,
                };

                house.HouseAmenities.Add(houseAmenity);


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
                    Name = FileManager.Save(imgFile, _env.WebRootPath, "uploads/houses"),
                    PosterStatus = false
                };
                house.HouseImages.Add(houseImage);

            }

            _context.Houses.Add(house);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.City = _context.Cities.ToList();
            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Manager = _context.SaleManagers.ToList();
            ViewBag.Amenities = _context.Amenities.ToList();


            var house = _context.Houses.Include(x=>x.HouseImages).FirstOrDefault(x => x.Id == id);
            house.AmenityIds = _context.HouseAmenities.Select(x => x.AmenityId).ToList();
                
            if (house == null)
                return RedirectToAction("error", "dashboard");


            return View(house);
        }
        [HttpPost]  
        public IActionResult Edit(House house)
        {
            _checkImageFiles(house.PosterImage, house.ImageFiles);

            if (!ModelState.IsValid)
            {
                ViewBag.City = _context.Cities.ToList();
                ViewBag.Category = _context.Categories.ToList();
                ViewBag.Manager = _context.SaleManagers.ToList();
                ViewBag.Amenities = _context.Amenities.ToList();

                return View();
            }

            var existhouse = _context.Houses.Include(x=>x.HouseImages).FirstOrDefault(x => x.Id == house.Id);

            if (existhouse == null)
                return RedirectToAction("error", "dashboard");

            if (house.PosterImage != null)
            {
                var poster = existhouse.HouseImages.FirstOrDefault(x => x.PosterStatus = true);

                var newimageName = FileManager.Save(house.PosterImage, _env.WebRootPath, "uploads/houses");

                FileManager.Delete(_env.WebRootPath, "uploads/houses", poster.Name);

                poster.Name = newimageName; 
            }

            var removedHouseImgs = existhouse.HouseImages.FindAll(x => x.PosterStatus == false);

            _context.HouseImages.RemoveRange(removedHouseImgs);

            foreach (var item in removedHouseImgs)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/houses", item.Name);
            }

            if (house.ImageFiles != null)
            {
                foreach (var imgFile in house.ImageFiles)
                {
                    HouseImage houseImage = new HouseImage
                    {
                        Name = FileManager.Save(imgFile, _env.WebRootPath, "uploads/houses")
                    };

                existhouse.HouseImages.Add(houseImage);
                }
            }


            foreach (var amenityId in house.AmenityIds)
            {
                HouseAmenity houseAmenity = new HouseAmenity
                {
                    AmenityId = amenityId,
                    HouseId=house.Id
                };
                existhouse.HouseAmenities.Add(houseAmenity);

            }

            existhouse.Name = house.Name;
            existhouse.ManagerId = house.ManagerId;
            existhouse.CityId = house.CityId;
            existhouse.CategoryId = house.CategoryId;
            existhouse.IsNew = house.IsNew;
            existhouse.IsSpecial = house.IsSpecial;
            existhouse.Description = house.Description;
            existhouse.RoomCount = house.RoomCount;
            existhouse.BathroomCount = house.BathroomCount;
            existhouse.BedroomCount = house.BedroomCount;
            existhouse.Area = house.Area;
            existhouse.SalePrice = house.SalePrice;
            existhouse.CostPrice = house.CostPrice;
            existhouse.DiscountPercantage = house.DiscountPercantage;
            existhouse.BuildYear = house.BuildYear;
            existhouse.StockStatus = house.StockStatus;

            _context.SaveChanges();


            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            var house=_context.Houses.Include(x=>x.HouseImages).FirstOrDefault(x=>x.Id==id);
                
            if (house == null)
                RedirectToAction("error", "dashboard");

            var poster = _context.HouseImages.FirstOrDefault(x => x.PosterStatus ==true);

            FileManager.Delete(_env.WebRootPath, "uploads/houses", poster.Name);

            var removedFiles = house.HouseImages.FindAll(x => x.PosterStatus == false);

            _context.HouseImages.RemoveRange(removedFiles);

            foreach (var imgFile in removedFiles)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/houses", imgFile.Name);
            }



            _context.Houses.Remove(house);
            _context.SaveChanges();

            return RedirectToAction("index");
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
