using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Woodstock.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public virtual IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
    }
}
