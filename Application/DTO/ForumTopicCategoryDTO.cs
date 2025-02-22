namespace Application.DTO
{
    public class QueryForumTopicCategoryDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
    public class CommandForumTopicCategoryDTO
    {
        public string CategoryName { get; set; }
    }
}
