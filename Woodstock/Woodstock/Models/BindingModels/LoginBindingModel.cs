using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.BindingModels
{
    public class LoginBindingModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("Электронная почта")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
