namespace TempusHiring.DataAccess.Entities
{
    public class OrderWatchLink
    {
        public int Id { get; set; }
        public int Count { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int WatchId { get; set; }
        public virtual Watch Watch { get; set; }
    }
}
