using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace Woodstock.BLL.Extensions
{
    public static class IdentityExtensions
    {
        private const string SIGN_IN_FAILURE_MESSAGE = "Wrong login or password";

        public static void ThrowExceptionOnFailure(this IdentityResult result)
        {
            if (result.Succeeded) return;

            var errorsDiscription = result.Errors.Select(_ => _.Description);
            var errors = string.Join(Environment.NewLine, errorsDiscription);
            throw new Exception(errors);
        }

        public static void ThrowExceptionOnFailure(this SignInResult result)
        {
            if (result.Succeeded) return;

            throw new AuthenticationException(SIGN_IN_FAILURE_MESSAGE);
        }

        public static int GetId(this ClaimsPrincipal user) =>
            Int32.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
