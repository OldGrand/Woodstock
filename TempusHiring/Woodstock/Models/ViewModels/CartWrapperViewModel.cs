using System.Collections.Generic;

namespace Woodstock.PL.Models.ViewModels
{
    public class CartWrapperViewModel
    {
        public OrderSummaryViewModel OrderSummary { get; set; }
        public IEnumerable<ShoppingCartViewModel> ShoppingCarts { get; set; }
    }
}
