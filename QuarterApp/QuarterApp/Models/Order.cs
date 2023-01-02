using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        [MaxLength(50)]

        public string Fullname { get; set; }
        [MaxLength(100)]

        public string Email { get; set; }
        [MaxLength(250)]

        public string Address1 { get; set; }
        [MaxLength(250)]

        public string? Address2 { get; set; }
        [MaxLength(30)]

        public string City { get; set; }
        [MaxLength(20)]

        public string ZipCode { get; set; }
        [MaxLength(500)]

        public string? Note { get; set; }


        public AppUser? AppUser { get; set; }
        public List<OrderItem>? OrderItems { get; set; }
    }
}
