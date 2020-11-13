namespace Woodstock.DAL.Entities
{
    public class OrderWatchLink
    {
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int WatchId { get; set; }
        public virtual Watch Watch { get; set; }
    }
}
