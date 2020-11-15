using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Woodstock.BLL.DTOs;

namespace Woodstock.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<SignInResultDTO> LoginAsync(UserDTO userDTO, bool isPersistent, bool lockoutOnFailure);
        Task<IdentityResultDTO> RegisterAsync(UserDTO userDTO, string claimRole);
        Task CompleteEmailConfirmationAsync(string email, string confirmToken);
        Task LogoutAsync();
        AuthenticationProperties ConfigureExternalAuthentication(string provider, string redirectUrl);
        Task<SignInResultDTO> ExternalLoginAsync();
        Task<IdentityResultDTO> ExternalRegisterAsync(UserDTO userDTO);
        Task<string> GenerateEmailConfirmationAsync(string email);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<IdentityResultDTO> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO);
    }
}
