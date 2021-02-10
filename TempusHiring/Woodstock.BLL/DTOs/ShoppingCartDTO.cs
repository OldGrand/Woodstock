namespace Woodstock.BLL.DTOs
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public bool IsChecked { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int WatchId { get; set; }
        public WatchDTO Watch { get; set; }
    }
}
