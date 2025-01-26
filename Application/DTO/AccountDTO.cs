namespace Application.DTO
{
    public class AccountDTO
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
    }
    public class CreateAccountDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
        public required string Role { get; set; }
    }
    public class UpdateAccountDTO
    {
        public required Guid Id { get; set; }
        public required string Password { get; set; }
        public required string Username { get; set; }
    }
}
