using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
