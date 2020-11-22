using Woodstock.BLL.Pagination;

namespace Woodstock.PL.Models.ViewModels
{
    public class FilteredWatchViewModel
    {
        public PagedResult<WatchViewModel> PageResult { get; set; }
        public Filter Filter { get; set; }
    }
}
