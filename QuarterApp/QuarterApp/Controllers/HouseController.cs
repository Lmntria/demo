using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;

namespace QuarterApp.Controllers
{
    public class HouseController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly UserManager<AppUser> _manager;

        public HouseController(QuarterDbContext context,UserManager<AppUser> manager)
        {
            _context= context;
            _manager= manager;
        }
        public IActionResult Detail(int id)
        {

            House house = _context.Houses
                .Include(x=>x.City)
                .Include(x => x.HouseImages)
                .Include(x => x.HouseAmenities).ThenInclude(x => x.Amenity)
                .Include(x => x.Manager)
                .FirstOrDefault(x => x.Id == id);

            if (house == null)
                return RedirectToAction("error", "dashboard");

            HouseDetailVM detailVM = new HouseDetailVM
            {
                House = house,
                Amenities=_context.Amenities.ToList(),
                Houses=_context.Houses.ToList(),
            };


            return View(detailVM);
        }

        public async Task<IActionResult> AddToWishList(int houseId)
        {
            AppUser user = null;

            if (User.Identity.IsAuthenticated)
            {
                user = await _manager.FindByNameAsync(User.Identity.Name);
            }

            if (!_context.Houses.Any(x => x.Id == houseId && x.StockStatus))
                return NotFound();

            WishListVM wishList = new WishListVM();

            if (user != null)
            {
                WishListItem wishListItem=_context.WishListItems.FirstOrDefault(x=>x.HouseId==houseId && x.AppUserId==user.Id);

                if(wishListItem == null)
                {
                    wishListItem = new WishListItem
                    {
                        AppUserId=user.Id,
                        HouseId=houseId,
                    };

                    _context.WishListItems.Add(wishListItem);
                }

                _context.SaveChanges();

                var model = _context.WishListItems.Include(x => x.House).ThenInclude(x => x.HouseImages).Where(x => x.AppUserId == user.Id).ToList();


                foreach (var item in model)
                {
                    WishListItemVM wishListItemVM = new WishListItemVM
                    {
                        House = item.House,
                        Id = item.Id,
                    };

                    wishList.WishListItems.Add(wishListItemVM);
                    
                }
            }

            else
            {
                var wishListStr = HttpContext.Request.Cookies["wishList"];

                List<WishListCookieItemVM> cookieItems = null;

                if (wishListStr == null)
                {
                    cookieItems = new List<WishListCookieItemVM>();
                }
                else
                {
                    cookieItems = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(wishListStr);
                }

                WishListCookieItemVM wishlistCookieItem = cookieItems.FirstOrDefault(x => x.HouseId == houseId);

                if (wishlistCookieItem == null)
                {
                    wishlistCookieItem = new WishListCookieItemVM
                    {
                        HouseId=houseId,
                    };

                    cookieItems.Add(wishlistCookieItem);

                }

                var jsonStr = JsonConvert.SerializeObject(cookieItems);
                HttpContext.Response.Cookies.Append("wishList", jsonStr);

                foreach (var item in cookieItems)
                {
                    House house = _context.Houses.Include(x => x.HouseImages).FirstOrDefault(x =>x.Id==item.HouseId);

                    WishListItemVM wishListItem = new WishListItemVM
                    {
                        House = house,
                        Id = 0,
                    };

                    wishList.WishListItems.Add(wishListItem);

                }
            }
            return RedirectToAction("index", "home");

        }
    }
}
