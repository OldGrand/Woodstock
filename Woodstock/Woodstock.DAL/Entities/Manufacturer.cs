using Woodstock.DAL.AbstractEntities;

namespace Woodstock.DAL.Entities
{
    public class Manufacturer : BaseIdTitle
    {
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
