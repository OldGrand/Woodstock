using Microsoft.AspNetCore.Mvc.Rendering;
using Woodstock.BLL.Pagination;

namespace Woodstock.PL.Models.ViewModels
{
    public class FilteredWatchViewModel
    {
        public PagedResult<WatchViewModel> PageResult { get; set; }
        public int ItemsOnPage { get; set; } = 12;
        public Filter Filter { get; set; }
        public SelectList ItemsOnPageVM { get; set; }
        public PriceRangeViewModel PriceRange { get; set; }
    }
}
