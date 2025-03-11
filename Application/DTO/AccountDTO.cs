namespace Application.DTO
{
    public class QueryAccountDTO
    {
        public required Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string CreatedDate { get; set; }
        public bool isRestricted { get; set; }
    }
    public class CommandAccountDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
