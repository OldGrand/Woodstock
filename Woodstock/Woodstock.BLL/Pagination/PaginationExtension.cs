using System;
using System.Linq;

namespace Woodstock.BLL.Pagination
{
    public static class PaginationExtension
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> items, int pageNum, int itemsOnPage) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = pageNum;
            result.ItemsOnPage = itemsOnPage;
            result.ItemsTotal = items.Count();
            result.SkippedItems = (pageNum - 1) * itemsOnPage;
            result.PagesTotal = (int)Math.Ceiling((double)result.ItemsTotal / itemsOnPage);
            result.Results = items.Skip(result.SkippedItems).Take(itemsOnPage).ToList();

            return result;
        }
    }
}
