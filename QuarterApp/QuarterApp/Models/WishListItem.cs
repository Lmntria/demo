namespace QuarterApp.Models
{
    public class WishListItem
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public string AppUserId { get; set; }
        public House House { get; set; }
        public AppUser AppUser { get; set; }
    }
}
