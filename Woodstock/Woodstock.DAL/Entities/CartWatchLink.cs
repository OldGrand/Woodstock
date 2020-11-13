namespace Woodstock.DAL.Entities
{
    public class CartWatchLink
    {
        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

        public int WatchId { get; set; }
        public virtual Watch Watch { get; set; }
    }
}
