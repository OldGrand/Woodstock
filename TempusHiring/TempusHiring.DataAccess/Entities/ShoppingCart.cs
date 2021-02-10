namespace TempusHiring.DataAccess.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public bool IsChecked { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int WatchId { get; set; }
        public virtual Watch Watch { get; set; }
    }
}
