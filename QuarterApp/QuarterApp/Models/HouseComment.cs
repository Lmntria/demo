namespace QuarterApp.Models
{
    public class HouseComment
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int HouseId { get; set; }
        public string Text { get; set; }
        public AppUser AppUser { get; set; }
        public House House { get; set; }
    }
}
