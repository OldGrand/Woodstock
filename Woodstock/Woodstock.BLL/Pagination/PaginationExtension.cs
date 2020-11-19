using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Woodstock.BLL.Pagination
{
    public static class PaginationExtension
    {
        public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> items, int pageNum, int itemsOnPage) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = pageNum;
            result.PageSize = itemsOnPage;
            result.RowCount = await items.CountAsync();
           
            result.PageCount = (int)Math.Ceiling((double)result.RowCount / itemsOnPage);
            result.Results = await items.Skip((pageNum - 1) * itemsOnPage).Take(itemsOnPage).ToListAsync();

            return result;
        }
    }
}
