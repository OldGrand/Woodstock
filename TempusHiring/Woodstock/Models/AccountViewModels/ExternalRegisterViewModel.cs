using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.AccountViewModels
{
    public class ExternalRegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}
