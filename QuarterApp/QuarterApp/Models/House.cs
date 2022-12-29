using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuarterApp.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuarterApp.Models
{
    public class House
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        public int? ParkingCount { get; set; }
        public int RoomCount { get; set; }
        public int BathroomCount { get; set; }
        public int BedroomCount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercantage { get; set; }
        public DateTime BuildYear { get; set; }

        [Column(TypeName = "decimal(18,2)")]

        public decimal Area { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Latitude { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Longitude { get; set; }
        public bool IsNew { get; set; }
        public bool IsSpecial { get; set; }
        public bool StockStatus { get; set; }
        public List<HouseImage>? HouseImages { get; set; }
        public SaleManager? Manager { get; set; }
        public Category? Category { get; set; }
        public City? City { get; set; }

        [NotMapped]
        [MaxFileSize(3)]
        [AllowedFileType("image/png", "image/jpeg", "image/jpg")]
        public IFormFile? PosterImage { get; set; }
        [NotMapped]
        [MaxFileSize(3)]
        [AllowedFileType("image/png", "image/jpeg", "image/jpg")]

        public List<IFormFile>? ImageFiles { get; set; }

        public List<HouseAmenity>? HouseAmenities { get; set; }
        [NotMapped]
        public List<int>? AmenityIds { get; set; } = new List<int>();
    }
}
