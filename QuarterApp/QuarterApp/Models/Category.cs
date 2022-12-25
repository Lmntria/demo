using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }    
        public List<House>? Houses { get; set; }
    }
}
