﻿using System;
using System.Collections.Generic;

namespace Woodstock.PL.Models.ViewModels
{
    public class PagedResultViewModel<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public Range Range { get; set; }
        public IList<T> Results { get; set; }
    }
}
