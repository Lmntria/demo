using System.ComponentModel.DataAnnotations;

namespace QuarterApp.ViewModels
{
    public class PasswordResetVm
    {
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match!!!")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
