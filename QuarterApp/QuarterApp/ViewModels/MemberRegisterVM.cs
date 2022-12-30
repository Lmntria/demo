using System.ComponentModel.DataAnnotations;

namespace QuarterApp.ViewModels
{
    public class MemberRegisterVM
    {
        [MaxLength(50)]
        public string Fullname { get; set; }
        [MaxLength(50)]

        public string Username { get; set;}
        [MaxLength(100)]

        public string Email { get; set;}
        [MaxLength(20)]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password do not match!!!")]
        public string ConfirmPassword { get; set; }
        public bool IsConsent { get; set; }
        public bool IsCreateConsent { get; set; }
    }
}
