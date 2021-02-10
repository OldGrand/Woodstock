namespace Woodstock.BLL.DTOs.Account
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
