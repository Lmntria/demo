using System.ComponentModel.DataAnnotations;

namespace QuarterApp.ViewModels
{
    public class ForgotPasswordVM
    {
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
