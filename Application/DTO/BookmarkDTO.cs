namespace Application.DTO
{
    public class QueryBookmarkDTO
    {
        public required Guid Id { get; set; }
        public required string CreatedDate { get; set; }
        public required string BookmarkType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Chapters { get; set; }
        public string ThemeURL { get; set; }
        public string Status { get; set; }
    }
    public class CommandBookmarkDTO
    {
        public required Guid AccountId { get; set; }
        public required Guid ComicId { get; set; }
        public required string BookmarkType { get; set; }
    }
}
