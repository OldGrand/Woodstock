using System.Collections.Generic;
using Woodstock.BLL.Abstract;

namespace Woodstock.BLL.Pagination
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}
