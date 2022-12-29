using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        public IActionResult Index(List<int>? cityIds=null,List<int>? categoryIds=null,List<int> salemanagerIds=null,List<int> amenityIds=null,decimal? minPrice=null,decimal? maxPrice=null)
        {
            ViewBag.SelectedCityIds = cityIds;
            ViewBag.SelectedCategoryIds = categoryIds;
            ViewBag.SelectedManagerIds= salemanagerIds;
            ViewBag.SelectedAmenityIds = amenityIds;


            var house = _context.Houses
            .Include(x => x.City)
            .Include(x => x.HouseImages)
            .Include(x => x.HouseAmenities).ThenInclude(x => x.Amenity)
            .Include(x => x.Manager).AsQueryable();

            if (cityIds != null && cityIds.Count>0)
            {
                house = house.Where(x => cityIds.Contains(x.CityId));
            }
            if (categoryIds != null && categoryIds.Count>0)
            {
                house = house.Where(x => categoryIds.Contains(x.CategoryId));
            }
            if (salemanagerIds != null && salemanagerIds.Count>0)
            {
                house = house.Where(x => salemanagerIds.Contains(x.ManagerId));
            }
            if (amenityIds != null && amenityIds.Count > 0)
            {
                house = house.Where(x => x.HouseAmenities.Any(ha => amenityIds.Contains(ha.AmenityId)));
            }
            if(minPrice!=null && maxPrice != null)
            {
                house = house.Where(x => x.SalePrice >= minPrice && x.SalePrice <= maxPrice);
            }



            ShopVm shopVm = new ShopVm
            {
                Houses = house.ToList(),
                Cities = _context.Cities.Include(x => x.Houses).ToList(),
                Categories = _context.Categories.Include(x => x.Houses).ToList(),
                Amenities = _context.Amenities.Include(x => x.HouseAmenities).ThenInclude(x => x.House).ToList(),
                SaleManagers=_context.SaleManagers.Include(x=>x.Houses).ToList(),
                MinPrice=_context.Houses.Min(x=>x.SalePrice),
                MaxPrice=_context.Houses.Max(x=>x.SalePrice),
            };

            ViewBag.SelectedMinPrice = minPrice?? shopVm.MinPrice;
            ViewBag.SelectedMaxPrice = maxPrice ?? shopVm.MaxPrice;

            return View(shopVm);
        }
    }
}
