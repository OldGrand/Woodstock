using System.Linq;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Extensions
{
    public static class CatalogExtension
    {
        public static IQueryable<WatchDTO> ReadByGender(this IQueryable<WatchDTO> source, string gender)
        {
            return from watch in source
                   where watch.Gender.Title == gender
                   select watch;
        }
    }
}
