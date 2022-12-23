using QuarterApp.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuarterApp.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Tittle1 { get; set; }
		[MaxLength(100)]
		public string Tittle2 { get; set; }
		[MaxLength(250)]
		public string Desc { get; set; }
		[MaxLength(80)]
		public string FirstBtnText { get; set; }
		[MaxLength(100)]
		public string FirstBtnUrl { get; set; }
		[MaxLength(80)]
		public string? SecondBtnText { get; set; }
		[MaxLength(100)]
		public string? SecondBtnUrl { get; set;}
		[MaxLength(100)]
		public string? Image { get; set; }
        public byte Order { get; set; }
		[NotMapped]
		[AllowedFileType("image/png", "image/jpeg")]
		[MaxFileSize(3)]
        public IFormFile? ImageFile { get; set; }
    }
}
