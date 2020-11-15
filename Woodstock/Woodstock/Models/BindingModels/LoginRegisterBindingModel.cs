namespace Woodstock.PL.Models.BindingModels
{
    public class LoginRegisterBindingModel
    {
        public LoginBindingModel Login { get; set; }
        public RegisterBindingModel Register { get; set; }
        public string ReturnUrl { get; set; }
    }
}
