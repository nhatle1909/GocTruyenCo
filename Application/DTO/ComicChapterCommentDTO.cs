namespace Application.DTO
{
    public class QueryComicChapterCommentDTO
    {
        public string AccountName { get; set; }
        public string ChapterName { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class CommandComicChapterCommentDTO
    {
        public Guid AccountId { get; set; }
        public Guid ComicChapterId { get; set; }
        public string Comment { get; set; }
    }
}
