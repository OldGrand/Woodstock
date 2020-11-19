using Woodstock.DAL.AbstractEntities;

namespace Woodstock.DAL.Entities
{
    public class Mechanism : IdTitleBased
    {
        public string Description { get; set; }
        public int PowerReserveDays { get; set; }

        //public int ManufacturerId { get; set; }
        //public virtual Manufacturer Manufacturer { get; set; }
        public int MechanismTypeId { get; set; }
        public virtual MechanismType MechanismType { get; set; }
    }
}
