namespace Woodstock.BLL.DTOs
{
    public sealed record UserDTO
    {
        public string Email { get; }
        public string UserName { get; }
        public string Password { get; }

        public UserDTO(string email, string userName, string password)
        {
            Email = email;
            UserName = userName;
            Password = password;
        }
    }
}
