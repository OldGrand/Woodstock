using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Woodstock.BLL.DTOs;
using Woodstock.BLL.Interfaces;
using Woodstock.PL.Models.BindingModels;

namespace Woodstock.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, IMapper mapper, IEmailService emailService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public IActionResult LoginRegister(string returnUrl = "/")
        {
            return View(new LoginRegisterBindingModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRegisterBindingModel loginRegisterBM)
        {
            if (!ModelState.IsValid)
                return View(nameof(LoginRegister), loginRegisterBM);

            var userDTO = _mapper.Map<UserDTO>(loginRegisterBM.Login);
            var signInResult = await _accountService.LoginAsync(userDTO, loginRegisterBM.Login.RememberMe, false);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                return View(nameof(LoginRegister), loginRegisterBM);
            }

            return Redirect(loginRegisterBM.ReturnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(LoginRegisterBindingModel loginRegisterBM)
        {
            if (!ModelState.IsValid)
                return View(nameof(LoginRegister), loginRegisterBM);

            var userDTO = _mapper.Map<UserDTO>(loginRegisterBM.Register);
            var identityResult = await _accountService.RegisterAsync(userDTO, ClaimRoles.User);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(nameof(LoginRegister), loginRegisterBM);
            }

            return await SendEmailAsync(userDTO, loginRegisterBM.Register.Email, loginRegisterBM.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string email, string confirmToken, string returnUrl)
        {
            try
            {
                await _accountService.CompleteEmailConfirmationAsync(email, confirmToken);
                return Redirect(returnUrl);
            }
            catch (Exception e)
            {
                return PartialView("_NotificationPartial", e.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(nameof(ExternalSignIn), "Account", new { returnUrl });
            var properties = _accountService.ConfigureExternalAuthentication(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        public async Task<IActionResult> ExternalSignIn(string returnUrl)
        {
            var signInResult = await _accountService.ExternalLoginAsync();

            if (signInResult.Succeeded)
                return Redirect(returnUrl);
            return View(nameof(ExternalRegistration), new ExternalRegisterBindingModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalRegistration(ExternalRegisterBindingModel extRegisterBM)
        {
            var userDTO = _mapper.Map<UserDTO>(extRegisterBM);
            var extRegisterIdentityResult = await _accountService.ExternalRegisterAsync(userDTO);

            if (!extRegisterIdentityResult.Succeeded)
            {
                foreach (var error in extRegisterIdentityResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(extRegisterBM);
            }

            return await SendEmailAsync(userDTO, extRegisterBM.Email, extRegisterBM.ReturnUrl);
        }

        private async Task<IActionResult> SendEmailAsync(UserDTO userDTO, string email, string returnUrl)
        {
            var confirmToken = await _accountService.RequestEmailConfirmationAsync(userDTO);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
                                         new { email, confirmToken, returnUrl },
                                         HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(email, "Confirm your account",
                $"Чтобы закончить регистрацию - перейдите по <a href='{callbackUrl}'>ссылке</a>");

            return PartialView("_NotificationPartial", "Check your email");
        }
    }
}