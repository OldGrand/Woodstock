namespace Woodstock.PL.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public int WatchId { get; set; }
        public WatchViewModel Watch { get; set; }
    }
}
