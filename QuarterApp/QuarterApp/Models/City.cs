using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Models
{
    public class City
    {
        public int Id { get; set; }
        [MaxLength(60)]
        public string Name { get; set; }
    }
}
