using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woodstock.BLL.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsOrderCompleted { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public IEnumerable<OrderWatchLinkDTO> OrderWatchLinks { get; set; }
    }
}
