using System;

namespace Woodstock.BLL.Abstract
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }//текущая страница currentPage
        public int PageCount { get; set; }//общее кол-во страниц 
        public int PageSize { get; set; }//кол-во элементов на странице
        public int RowCount { get; set; }//общее кол-во элементов total
        
        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}
