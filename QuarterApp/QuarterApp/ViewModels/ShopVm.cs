using QuarterApp.Models;

namespace QuarterApp.ViewModels
{
    public class ShopVm
    {
        public List<House> Houses { get; set; }
        public List<City> Cities { get; set; }
        public List<Category> Categories { get; set; }
        public List<Amenity> Amenities { get; set; }
        public List<SaleManager> SaleManagers { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
