using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;
using System.Security.Claims;

namespace QuarterApp.Services
{
    public class LayoutService
    {
        private readonly QuarterDbContext _context;
        private readonly IHttpContextAccessor _httpAccessor;

        public LayoutService(QuarterDbContext context,IHttpContextAccessor httpAccessor)
        {
            _context= context;
            _httpAccessor= httpAccessor;
        }

        public Dictionary<string, string> GetSettings() 
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }

        public WishListVM GetWishListItems()
        {
            WishListVM wishList = new WishListVM();

            if (_httpAccessor.HttpContext.User.Identity.IsAuthenticated && _httpAccessor.HttpContext.User.IsInRole("Member"))
            {
                string userId = _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var model = _context.WishListItems.Include(x=>x.House).ThenInclude(x=>x.HouseImages).Where(x=>x.AppUserId==userId).ToList();

                foreach(var item in model)
                {
                    WishListItemVM wishListItemVM = new WishListItemVM
                    {
                        House=item.House,
                        Id=item.Id,
                    };

                    wishList.WishListItems.Add(wishListItemVM);

                }
            }
            else
            {
                var wishListStr = _httpAccessor.HttpContext.Request.Cookies["wishList"];

                List<WishListCookieItemVM> cookieItems = new List<WishListCookieItemVM>();

                if (wishListStr != null)
                {
                    cookieItems = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(wishListStr);

                }

                foreach (var item in cookieItems)
                {
                    House house = _context.Houses.Include(x => x.HouseImages).FirstOrDefault(x => x.Id == item.HouseId);

                    WishListItemVM wishListItem = new WishListItemVM
                    {
                        House = house,
                        Id = 0,
                    };

                    wishList.WishListItems.Add(wishListItem);

                }
            }

            return wishList;
        }
    }
}
