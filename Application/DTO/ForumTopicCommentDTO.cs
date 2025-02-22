namespace Application.DTO
{
    public class QueryForumTopicCommentDTO
    {
        public Guid Id { get; set; }
        public string AccountName { get; set; }
        public string CreatedDate { get; set; }
        public string Comment { get; set; }
    }
    public class CommandForumTopicCommentDTO
    {
        public required Guid ForumTopicId { get; set; }
        public required Guid AccountId { get; set; }
        public required string Comment { get; set; }
    }
}
