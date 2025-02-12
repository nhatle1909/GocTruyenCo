namespace Application.DTO
{
    public class QueryAccountDTO
    {
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
    }
    public class CommandAccountDTO
    {
  
        public required string Username { get; set; }
        public required string Password { get; set; }
     
    }
}
