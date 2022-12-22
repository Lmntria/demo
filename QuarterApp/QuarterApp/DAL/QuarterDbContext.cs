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
	}
}
