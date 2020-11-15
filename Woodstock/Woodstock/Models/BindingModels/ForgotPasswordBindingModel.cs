using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.BindingModels
{
    public class ForgotPasswordBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
