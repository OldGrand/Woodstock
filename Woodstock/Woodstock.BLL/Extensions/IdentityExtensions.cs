using System;
using System.Security.Claims;

namespace Woodstock.BLL.Extensions
{
    public static class IdentityExtensions
    {
        public static int GetId(this ClaimsPrincipal user) =>
            Int32.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
