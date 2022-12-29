using Microsoft.AspNetCore.Identity;

namespace QuarterApp.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
