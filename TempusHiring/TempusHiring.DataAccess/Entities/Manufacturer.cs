using TempusHiring.DataAccess.AbstractEntities;
using TempusHiring.DataAccess.EntityEnums;

namespace TempusHiring.DataAccess.Entities
{
    public class Manufacturer : IdTitleBased
    {
        public Country Country { get; set; }
    }
}
