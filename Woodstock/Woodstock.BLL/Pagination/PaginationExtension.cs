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
            result.PageSize = itemsOnPage;
            result.RowCount = items.Count();
           
            result.PageCount = (int)Math.Ceiling((double)result.RowCount / itemsOnPage);
            result.Results = items.Skip((pageNum - 1) * itemsOnPage).Take(itemsOnPage).ToList();

            return result;
        }
    }
}
