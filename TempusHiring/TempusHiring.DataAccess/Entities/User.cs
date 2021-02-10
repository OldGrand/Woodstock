using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TempusHiring.DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        public virtual IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    }
}
