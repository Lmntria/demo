using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Models
{
    public class Setting
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Key { get; set; }
        [MaxLength(100)]
        public string Value { get; set; }
    }
}
