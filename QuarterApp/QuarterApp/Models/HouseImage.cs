using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Models
{
    public class HouseImage
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        public bool PosterStatus { get; set; }
        public  House House { get; set; }
    }
}
