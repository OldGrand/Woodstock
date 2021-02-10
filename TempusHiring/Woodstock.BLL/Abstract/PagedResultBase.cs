using System;

namespace Woodstock.BLL.Abstract
{
    public abstract class PagedResultBase
    {
        private const int PAGES_RANGE = 3;

        public int CurrentPage { get; set; }
        public int PagesTotal { get; set; }//общее кол-во страниц 
        public int ItemsOnPage { get; set; }
        public int ItemsTotal { get; set; }//общее кол-во элементов total
        public int SkippedItems { get; set; }//кол-во просмотренных элементов
        public int CurrentItemsCount => SkippedItems + ItemsOnPage;
        public Range Range => GetPaginationRange(CurrentPage, PagesTotal, PAGES_RANGE);

        private static Range GetPaginationRange(int currentPage, int total, int range)
        {
            if (range > total)
                return 1..total;

            var middleOfRange = range / 2;
            var start = currentPage - middleOfRange;
            var end = currentPage + middleOfRange;

            if (start <= 0)
            {
                start = 1;
                end = currentPage + range - 1;
            }

            if (end > total)
            {
                end = total;
                start = currentPage - range + 1;
            }

            return start..end;
        }
    }
}
