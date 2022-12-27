using QuarterApp.Models;

namespace QuarterApp.ViewModels
{
	public class HomeVM
	{
		public List<Slider> Sliders { get; set; }
		public List<City> Cities { get; set; }
		public List<Category> Categories { get; set; }
		public List<House> Houses { get; set; }
		public List<Amenity> Amenities { get; set; }
		public List<OurService> Services { get; set; }
    }
}
