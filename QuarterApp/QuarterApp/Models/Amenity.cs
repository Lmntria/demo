using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Models
{
    public class Amenity
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(80)]
        public string Icon { get; set; }

        public List<HouseAmenity>? HouseAmenities { get; set; }
    }
}
