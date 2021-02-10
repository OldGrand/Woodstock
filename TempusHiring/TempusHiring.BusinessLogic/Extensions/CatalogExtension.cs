using System;
using System.Linq;
using TempusHiring.BusinessLogic.DTOs;
using TempusHiring.DataAccess.EntityEnums;

namespace TempusHiring.BusinessLogic.Extensions
{
    public static class CatalogExtension
    {
        public static IQueryable<WatchDTO> ReadByGender(this IQueryable<WatchDTO> source, Gender gender)
        {
            return from watch in source
                   where watch.Gender == gender
                   select watch;
        }

        public static IQueryable<WatchDTO> GetPriceInRange(this IQueryable<WatchDTO> source, int start, int end)
        {
            if (start > end)
                throw new Exception("Range start cannot be more than the end");

            return from watch in source
                   where watch.Price >= start && watch.Price <= end
                   select watch;
        }
    }
}
