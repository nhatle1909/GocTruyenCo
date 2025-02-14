namespace Application.DTO
{
    public class QueryComicChapterDTO
    {
        public required Guid Id { get; set; }
        public required Guid ComicId { get; set; }
        public required string Name { get; set; }
    }
    public class CommandComicChapterDTO
    {
        public required Guid ComicId { get; set; }
        public required string Name { get; set; }
    }
}
