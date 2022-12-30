using System.ComponentModel.DataAnnotations;

namespace QuarterApp.ViewModels
{
    public class MemberLoginVM
    {
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
