using System;
using System.Collections.Generic;

namespace Woodstock.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsOrderCompleted { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<OrderWatchLink> OrderWatchLinks { get; set; }
    }
}
