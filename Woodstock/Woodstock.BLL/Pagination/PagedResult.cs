using System.Collections.Generic;
using System.Linq;
using Woodstock.BLL.Abstract;

namespace Woodstock.BLL.Pagination
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IEnumerable<T> Results { get; set; }

        public PagedResult() => Results = Enumerable.Empty<T>();
    }
}
