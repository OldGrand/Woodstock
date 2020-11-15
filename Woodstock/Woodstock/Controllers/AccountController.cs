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

            try
            {
                var confirmToken = await _accountService.GenerateEmailConfirmationAsync(userDTO.Email);
                return await SendConfirmEmailAsync(confirmToken, userDTO.Email, loginRegisterBM.ReturnUrl);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(loginRegisterBM);
            }
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

            try
            {
                var confirmToken = await _accountService.GenerateEmailConfirmationAsync(userDTO.Email);
                return await SendConfirmEmailAsync(confirmToken, userDTO.Email, extRegisterBM.ReturnUrl);
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(extRegisterBM);
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordBindingModel forgotPasswordBM)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordBM);

            try
            {
                var resetToken = await _accountService.GeneratePasswordResetTokenAsync(forgotPasswordBM.Email);
                var callbackUrl = Url.Action(nameof(ResetPassword), "Account", 
                                             new { resetToken, forgotPasswordBM.Email }, HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(forgotPasswordBM.Email, "Reset Password",
                    $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
            }
            catch {}

            return PartialView("_NotificationPartial", "Check your email");
        }
        
        [HttpGet]
        public IActionResult ResetPassword(string resetToken = null, string email = null)
        {
            return View(new ResetPasswordBindingModel { Email = email, ResetToken = resetToken });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordBindingModel resetPasswordBM)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordBM);

            var resetPasswordDTO = _mapper.Map<ResetPasswordDTO>(resetPasswordBM);
            var identityResult = await _accountService.ResetPasswordAsync(resetPasswordDTO);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(resetPasswordBM);
            }

            return PartialView("_NotificationPartial", "Пароль успешно изменен");
        }
        
        private async Task<IActionResult> SendConfirmEmailAsync(string confirmToken, string email, string returnUrl)
        {
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
                                         new { email, confirmToken, returnUrl }, HttpContext.Request.Scheme);

            await _emailService.SendEmailAsync(email, "Confirm your account",
                $"Чтобы закончить регистрацию - перейдите по <a href='{callbackUrl}'>ссылке</a>");

            return PartialView("_NotificationPartial", "Check your email");
        }
    }
}