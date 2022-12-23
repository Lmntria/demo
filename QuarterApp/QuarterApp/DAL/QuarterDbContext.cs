using QuarterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace QuarterApp.DAL
{
	public class QuarterDbContext:DbContext
	{
		public QuarterDbContext(DbContextOptions<QuarterDbContext> opt):base(opt)
		{

		}

		public DbSet<Slider> Sliders { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<SaleManager> SaleManagers { get; set; }
		public DbSet<Setting> Settings { get; set; }
	}
}
