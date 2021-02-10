using System;
using System.Collections.Generic;

namespace Woodstock.PL.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsOrderCompleted { get; set; }

        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public IEnumerable<OrderWatchLinkViewModel> OrderWatchLinks { get; set; }
    }
}
