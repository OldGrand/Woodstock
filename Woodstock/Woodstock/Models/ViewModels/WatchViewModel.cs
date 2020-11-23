using Woodstock.DAL.Entities;

namespace Woodstock.PL.Models.ViewModels
{
    public class WatchViewModel
    {
        public double Diameter { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
    }
}
