using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.BindingModels
{
    public class ResetPasswordBindingModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Пароль должен содержать как минимум 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string ResetToken { get; set; }
    }
}
