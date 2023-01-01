using QuarterApp.Models;
using System.Diagnostics.Contracts;

namespace QuarterApp.ViewModels
{
    public class WishListItemVM
    {
        public int Id { get; set; }
        public House House { get; set; }
    }
}
