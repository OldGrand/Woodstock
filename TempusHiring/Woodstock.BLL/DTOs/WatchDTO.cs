﻿using Infrastructure.Enums;

namespace Woodstock.BLL.DTOs
{
    public class WatchDTO
    {
        public int Id { get; set; }
        public double Diameter { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public Gender Gender { get; set; }
        public int CountInStock { get; set; }
        public int SaledCount { get; set; }
    }
}


