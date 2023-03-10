using QuarterApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace QuarterApp.DAL
{
	public class QuarterDbContext:IdentityDbContext
	{
		public QuarterDbContext(DbContextOptions<QuarterDbContext> opt):base(opt)
		{

		}

		public DbSet<Slider> Sliders { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<SaleManager> SaleManagers { get; set; }
		public DbSet<Setting> Settings { get; set; }
		public DbSet<HouseImage> HouseImages { get; set; }
		public DbSet<House> Houses { get; set; }
		public DbSet<Amenity> Amenities { get; set; }
		public DbSet<HouseAmenity> HouseAmenities { get;set; }
		public DbSet<OurService> OurServices { get; set; }
		public DbSet<AboutUs> AboutUs { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<WishListItem> WishListItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
		public DbSet<HouseComment> Comments { get; set; }
    }
}
