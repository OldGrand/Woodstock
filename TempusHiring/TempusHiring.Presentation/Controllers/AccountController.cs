using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TempusHiring.BusinessLogic.DTOs;
using TempusHiring.BusinessLogic.DTOs.Account;
using TempusHiring.BusinessLogic.Interfaces;
using TempusHiring.Common;
using TempusHiring.Presentation.Models.AccountViewModels;

namespace TempusHiring.Presentation.Controllers
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

        [HttpGet]
        public IActionResult Registration(string returnUrl = "/")
        {
            var registerViewModel = new RegisterViewModel { ReturnUrl = returnUrl };
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            var loginViewModel = new LoginViewModel { ReturnUrl = returnUrl };
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Login), loginViewModel);

            var loginModel = _mapper.Map<LoginDTO>(loginViewModel);

            try
            {
                await _accountService.LoginAsync(loginModel, false);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(nameof(Login), loginViewModel);
            }

            return Redirect(loginViewModel.ReturnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(nameof(Registration), registerViewModel);

            var registerModel = _mapper.Map<RegisterDTO>(registerViewModel);

            try
            {
                await _accountService.RegisterAsync(registerModel, ClaimRoles.User);

                var confirmToken = await _accountService.GenerateEmailConfirmationAsync(registerModel.Email);

                var callbackUrl = Url.Action("ConfirmEmail", "Account",
                             new { registerModel.Email, confirmToken, registerViewModel.ReturnUrl }, HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(registerModel.Email, "Confirm your action",
                    $"Чтобы завершить регистрацию - перейдите по <a href='{callbackUrl}'>ссылке</a>");

                return View("Notification", "Check your email");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(nameof(Registration), registerViewModel);
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
                return View("Notification", e.Message);
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
            try
            {
                await _accountService.ExternalLoginAsync();
                return Redirect(returnUrl);
            }
            catch
            {
                return View(nameof(ExternalRegistration), new ExternalRegisterViewModel { ReturnUrl = returnUrl });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalRegistration(ExternalRegisterViewModel externalRegisterViewModel)
        {
            if (!ModelState.IsValid)
                return View(externalRegisterViewModel);

            var userModel = _mapper.Map<UserDTO>(externalRegisterViewModel);

            try
            {
                await _accountService.ExternalRegisterAsync(userModel);

                var confirmToken = await _accountService.GenerateEmailConfirmationAsync(userModel.Email);

                var callbackUrl = Url.Action("ConfirmEmail", "Account",
                             new { userModel.Email, confirmToken, externalRegisterViewModel.ReturnUrl }, HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(userModel.Email, "Confirm your action",
                    $"Для подтверждения привязки стороннего сервиса к учетной записи {userModel.Email} - перейдите по <a href='{callbackUrl}'>ссылке</a>");

                return View("Notification", "Check your email");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(externalRegisterViewModel);
            }
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordViewModel);

            try
            {
                var resetToken = await _accountService.GeneratePasswordResetTokenAsync(forgotPasswordViewModel.Email);
                var callbackUrl = Url.Action(nameof(ResetPassword), "Account",
                                             new { resetToken, forgotPasswordViewModel.Email }, HttpContext.Request.Scheme);

                await _emailService.SendEmailAsync(forgotPasswordViewModel.Email, "Reset Password",
                    $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");

                return View("Notification", "Check your email");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(forgotPasswordViewModel);
            }
        }

        [HttpGet]
        public IActionResult ResetPassword(string resetToken = null, string email = null)
        {
            return View(new ResetPasswordViewModel { Email = email, ResetToken = resetToken });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
                return View(resetPasswordViewModel);

            var resetPasswordModel = _mapper.Map<ResetPasswordDTO>(resetPasswordViewModel);

            try
            {
                await _accountService.ResetPasswordAsync(resetPasswordModel);
                return View("Notification", "Password changed successfully");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(resetPasswordViewModel);
            }
        }
    }
}