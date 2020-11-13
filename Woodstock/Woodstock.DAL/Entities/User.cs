using Microsoft.AspNetCore.Identity;

namespace Woodstock.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public virtual Order Order { get; set; }
    }
}
