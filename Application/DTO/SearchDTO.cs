namespace Application.DTO
{
    public class SearchDTO
    {
        public required string[] searchFields { get; set; }
        public required string[] searchValues { get; set; }
        public string sortField { get; set; }
        public bool sortAscending { get; set; }
        public int pageSize { get; set; }
        public int skip { get; set; }
    }
}
