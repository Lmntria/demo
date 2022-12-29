using QuarterApp.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuarterApp.Models
{
    public class OurService
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string? Image { get; set; }
        [NotMapped]
        [MaxFileSize(3)]
        [AllowedFileType("image/png", "image/jpeg", "image/jpg")]

        public IFormFile? ImageFile { get; set; }
    }
}
