using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Areas.Manage.ViewModels
{
    public class AdminLoginVM
    {
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
