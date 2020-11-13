using System.Collections.Generic;

namespace Woodstock.DAL.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<CartWatchLink> CartWatchLinks { get; set; }
    }
}
