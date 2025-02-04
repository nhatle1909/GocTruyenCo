namespace Application.DTO
{
    public class CommandComicCategoryDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
    public class QueryComicCategoryDTO
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
