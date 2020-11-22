using Woodstock.DAL.Entities;

namespace Woodstock.BLL.DTOs
{
    public sealed record WatchDTO
    {
        public double Diameter { get; }
        public string Description { get; }
        public decimal Price { get; }
        public string Photo { get; }
        public string Title { get; }
        public int GenderId { get; }
        public Gender Gender { get; }

        public WatchDTO(double diameter, string description, decimal price, 
                        string photo, string title, Gender gender)
        {
            Diameter = diameter;
            Description = description;
            Price = price;
            Photo = photo;
            Title = title;
            Gender = gender;
            GenderId = Gender.Id;
        }
    }
}


