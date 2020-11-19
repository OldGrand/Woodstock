using System.Collections.Generic;
using Woodstock.DAL.AbstractEntities;

namespace Woodstock.DAL.Entities
{
    public class Watch : IdTitleBased
    {
        public double Diameter { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }

        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public int GlassMaterialId { get; set; }
        public virtual GlassMaterial GlassMaterial { get; set; }
        public int MechanismId { get; set; }
        public virtual Mechanism Mechanism { get; set; }
        public int BodyMaterialId { get; set; }
        public virtual BodyMaterial BodyMaterial { get; set; }
        public int StrapId { get; set; }
        public virtual Strap Strap { get; set; }
        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual IEnumerable<OrderWatchLink> OrderWatchLinks { get; set; }
        public virtual IEnumerable<CartWatchLink> CartWatchLinks { get; set; }
    }
}
