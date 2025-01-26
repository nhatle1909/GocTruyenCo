namespace Application.DTO
{
    public class AuthenticateDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
    public class SignUpDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
        public required string Role = "Reader";
    }
}
