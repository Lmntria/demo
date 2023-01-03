using QuarterApp.Models;
namespace QuarterApp.ViewModels
{
    public class HouseDetailVM
    {
        public House House { get; set; }
        public List<Amenity> Amenities { get; set; }
        public List<House> Houses { get; set; }
    }
}
