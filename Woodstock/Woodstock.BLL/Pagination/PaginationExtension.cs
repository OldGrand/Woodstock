using System;
using System.Linq;

namespace Woodstock.BLL.Pagination
{
    public static class PaginationExtension
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> source, int pageNum, int itemsOnPage) where T : class
        {
            int count = source.Count();
            int skippedItems = (pageNum - 1) * itemsOnPage;

            var result = new PagedResult<T>()
            {
                CurrentPage = pageNum,
                ItemsOnPage = itemsOnPage,
                ItemsTotal = count,
                SkippedItems = skippedItems,
                PagesTotal = (int)Math.Ceiling((double)count / itemsOnPage),
                Results = source.Skip(skippedItems).Take(itemsOnPage).ToList()
            };

            return result;
        }
    }
}
