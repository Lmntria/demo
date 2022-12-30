using System.ComponentModel.DataAnnotations;

namespace QuarterApp.ViewModels
{
    public class MemberUpdateVM
    {
        [MaxLength(25)]
        public string UserName { get; set; }
        [MaxLength(25)]
        public string? Fullname { get; set; }
        [MaxLength(100)]
        public string? Email { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!!!")]
        public string? ConfirmPassword { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
    }
}
