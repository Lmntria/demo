using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuarterApp.DAL;
using QuarterApp.Models;
using QuarterApp.ViewModels;
using System.Security.Claims;

namespace QuarterApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly QuarterDbContext _context;
        private readonly UserManager<AppUser> _manager;

        public OrderController(QuarterDbContext context, UserManager<AppUser> manager)
        {
            _context = context;
            _manager = manager;
        }
        public async Task<IActionResult> Checkout()
        {
            var model = await _getCheckoutVM();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (!ModelState.IsValid)
            {
                var model=await _getCheckoutVM();
                model.Order = order;

                return View(model);
            }

            List<WishListItem> wishListItems= new List<WishListItem>();

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                wishListItems=_context.WishListItems.Include(x=>x.House).Where(x=>x.AppUserId== userId).ToList();
                order.AppUserId = userId;
                _context.WishListItems.RemoveRange(wishListItems);
            }
            else
            {
                wishListItems = _mapWishlistItems(_getWishlistCookieItems());
                Response.Cookies.Delete("wishList");
            }

            order.OrderItems = wishListItems.Select(x => new OrderItem
            {
                HouseId = x.HouseId,
                SalePrice = x.House.SalePrice,
                CostPrice = x.House.CostPrice,
                DiscountPercantage = x.House.DiscountPercantage,
                Name = x.House.Name


            }).ToList();

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("index", "home");
        }


        private async Task<CheckoutVM> _getCheckoutVM()
        {
            CheckoutVM checkoutVM = new CheckoutVM();
            List<WishListItem> wishListItems= new List<WishListItem>();

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                AppUser user=await _manager.FindByIdAsync(userId);

                checkoutVM.Order = new Order
                {
                    Fullname = user.Fullname,
                    Email = user.Email,
                };

                wishListItems=_context.WishListItems.Include(x=>x.House).Where(x=>x.AppUserId== userId).ToList();
            }
            else
            {
                var cookieItems = _getWishlistCookieItems();
                wishListItems = _mapWishlistItems(cookieItems);
            }

            checkoutVM.Items = wishListItems.Select(x => new CheckoutItemVM
            {
                Name = x.House.Name,
                SalePrice=x.House.SalePrice,
                DiscountPercantage=x.House.DiscountPercantage,
            }).ToList();

            return checkoutVM;
        }



        private List<WishListCookieItemVM> _getWishlistCookieItems()
        {
            var wishlistStr = HttpContext.Request.Cookies["wishList"];

            List<WishListCookieItemVM> wishlistCookieItems = new List<WishListCookieItemVM>();
            if (wishlistStr != null)
            {
                wishlistCookieItems = JsonConvert.DeserializeObject<List<WishListCookieItemVM>>(wishlistStr);
            }

            return wishlistCookieItems;
        }

        private List<WishListItem> _mapWishlistItems(List<WishListCookieItemVM> wishlistCookieItems)
        {
            List<WishListItem> wishlitItems = new List<WishListItem>();
            foreach (var item in wishlistCookieItems)
            {
                House house = _context.Houses.FirstOrDefault(x => x.Id == item.HouseId && x.StockStatus);
                if (house == null) continue;

                WishListItem wishlistItem = new WishListItem
                {
                    HouseId=item.HouseId,
                    House=house,
                };

                wishlitItems.Add(wishlistItem);
            }

            return wishlitItems;
        }
    }
}
