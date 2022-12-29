using QuarterApp.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuarterApp.Models
{
    public class SaleManager    
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Fullname { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string? Image { get; set; }
        [NotMapped]
        [MaxFileSize(3)]
        [AllowedFileType("image/jpeg", "image/png")]
        public IFormFile? ImageFile { get; set; }

        public List<House>? Houses { get; set; }
    }
}
