using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Interfaces;
using Woodstock.DAL.Entities;

namespace Woodstock.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, 
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<SignInResult> LoginAsync(UserDTO userDTO, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            return await _signInManager.PasswordSignInAsync(user, userDTO.Password, isPersistent, lockoutOnFailure);
        }

        public async Task<IdentityResult> RegisterAsync(UserDTO userDTO, string claimRole)
        {
            var user = _mapper.Map<User>(userDTO);
            await _userManager.CreateAsync(user, userDTO.Password);
            return await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claimRole));
        }

        public async Task<string> RequestEmailConfirmationAsync(UserDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task CompleteEmailConfirmationAsync(string email, string confirmToken)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var confirmEmailIdentityResult = await _userManager.ConfirmEmailAsync(user, confirmToken);
            if (!confirmEmailIdentityResult.Succeeded)
                throw new Exception(confirmEmailIdentityResult.Errors.FirstOrDefault().Description);

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

        public async Task<SignInResult> ExternalLoginAsync()
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            return await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider, 
                                                                 externalLoginInfo.ProviderKey, 
                                                                 false, false);
        }

        public async Task<IdentityResult> ExternalRegisterAsync(UserDTO userDTO)
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var user = await _userManager.FindByEmailAsync(userDTO.Email);

            if (user is null)
            {
                user = _mapper.Map<User>(userDTO);
                await _userManager.CreateAsync(user);
            }

            return await _userManager.AddLoginAsync(user, externalLoginInfo);
        }
    }
}
