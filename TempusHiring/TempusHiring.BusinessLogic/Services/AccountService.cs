using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using TempusHiring.BusinessLogic.DTOs;
using TempusHiring.BusinessLogic.DTOs.Account;
using TempusHiring.BusinessLogic.Extensions;
using TempusHiring.BusinessLogic.Interfaces;
using TempusHiring.Common;
using TempusHiring.DataAccess.Entities;

namespace TempusHiring.BusinessLogic.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task LoginAsync(LoginDTO loginModel, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user is null)
            {
                throw new AuthenticationException("Wrong login or password");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password,
                                                                        loginModel.RememberMe, lockoutOnFailure);
            signInResult.ThrowExceptionOnFailure();
        }

        public async Task RegisterAsync(RegisterDTO registerModel, string claimRole = ClaimRoles.User)
        {
            var user = _mapper.Map<User>(registerModel);

            var createIdentityResult = await _userManager.CreateAsync(user, registerModel.Password);
            createIdentityResult.ThrowExceptionOnFailure();

            var identityResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claimRole));
            identityResult.ThrowExceptionOnFailure();
        }

        public async Task CompleteEmailConfirmationAsync(string email, string confirmToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var confirmEmailIdentityResult = await _userManager.ConfirmEmailAsync(user, confirmToken);
            confirmEmailIdentityResult.ThrowExceptionOnFailure();

            await _signInManager.SignInAsync(user, false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public AuthenticationProperties ConfigureExternalAuthentication(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }

        public async Task ExternalLoginAsync()
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var signInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                                                                 externalLoginInfo.ProviderKey,
                                                                 false, false);
            signInResult.ThrowExceptionOnFailure();
        }

        public async Task ExternalRegisterAsync(UserDTO userModel)
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var user = await _userManager.FindByEmailAsync(userModel.Email);

            if (user is null)
            {
                user = new User
                {
                    Email = userModel.Email,
                    UserName = userModel.UserName
                };
                await _userManager.CreateAsync(user);
            }

            var identityResult = await _userManager.AddLoginAsync(user, externalLoginInfo);
            identityResult.ThrowExceptionOnFailure();
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
                throw new Exception("User with specified e-mail not found");

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new Exception("User with specified e-mail not found");

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task ResetPasswordAsync(ResetPasswordDTO resetPasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            var identityResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.ResetToken, resetPasswordModel.Password);
            identityResult.ThrowExceptionOnFailure();
        }
    }
}