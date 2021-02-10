namespace TempusHiring.BusinessLogic.DTOs.Account
{
    public class ResetPasswordDTO
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ResetToken { get; set; }
    }
}
