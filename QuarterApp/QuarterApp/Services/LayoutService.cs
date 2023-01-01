using Microsoft.EntityFrameworkCore;
using QuarterApp.DAL;
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

            return wishList;
        }
    }
}
