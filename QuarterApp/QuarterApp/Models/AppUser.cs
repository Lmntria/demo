using Microsoft.AspNetCore.Identity;
using QuarterApp.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuarterApp.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        //public string? Image { get; set; }
        //[NotMapped]
        //[MaxFileSize(3)]
        //[AllowedFileType("image/png", "image/jpeg", "image/jpg")]
        //public IFormFile? ProfileImg { get; set; }
    }
}
