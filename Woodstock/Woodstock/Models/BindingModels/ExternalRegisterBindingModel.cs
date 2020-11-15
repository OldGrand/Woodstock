using System.ComponentModel.DataAnnotations;

namespace Woodstock.PL.Models.BindingModels
{
    public class ExternalRegisterBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
    }
}
