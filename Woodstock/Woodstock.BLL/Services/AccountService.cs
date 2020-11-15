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

        public async Task<SignInResultDTO> LoginAsync(UserDTO userDTO, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);

            if (user is null)
                user = _mapper.Map<User>(userDTO);
            
            var signInResult = await _signInManager.PasswordSignInAsync(user, userDTO.Password, 
                                                                        isPersistent, lockoutOnFailure);

            return _mapper.Map<SignInResultDTO>(signInResult);
        }

        public async Task<IdentityResultDTO> RegisterAsync(UserDTO userDTO, string claimRole)
        {
            var user = _mapper.Map<User>(userDTO);
            await _userManager.CreateAsync(user, userDTO.Password);
            var identityResult = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, claimRole));
            return _mapper.Map<IdentityResultDTO>(identityResult);
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

        public async Task<SignInResultDTO> ExternalLoginAsync()
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var signInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider, 
                                                                 externalLoginInfo.ProviderKey, 
                                                                 false, false);
            return _mapper.Map<SignInResultDTO>(signInResult);
        }

        public async Task<IdentityResultDTO> ExternalRegisterAsync(UserDTO userDTO)
        {
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            var user = await _userManager.FindByEmailAsync(userDTO.Email);

            if (user is null)
            {
                user = _mapper.Map<User>(userDTO);
                await _userManager.CreateAsync(user);
            }

            var identityResult = await _userManager.AddLoginAsync(user, externalLoginInfo);
            return _mapper.Map<IdentityResultDTO>(identityResult);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null || !await _userManager.IsEmailConfirmedAsync(user))
                throw new Exception("Пользователь с указанной почтой не найден");

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<string> GenerateEmailConfirmationAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new Exception("Пользователь с указанной почтой не найден");

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResultDTO> ResetPasswordAsync(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            var identityResult = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.ResetToken, resetPasswordDTO.Password);
            return _mapper.Map<IdentityResultDTO>(identityResult);
        }
    }
}
