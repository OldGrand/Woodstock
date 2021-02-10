using Infrastructure.Enums;
using Woodstock.DAL.AbstractEntities;

namespace Woodstock.DAL.Entities
{
    public class Manufacturer : IdTitleBased
    {
        public Country Country { get; set; }
    }
}
