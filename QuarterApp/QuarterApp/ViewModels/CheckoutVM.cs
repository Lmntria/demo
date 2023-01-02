using QuarterApp.Models;

namespace QuarterApp.ViewModels
{
    public class CheckoutVM
    {
        public Order Order { get; set; }
        public List<CheckoutItemVM> Items { get; set; }
    }
}
