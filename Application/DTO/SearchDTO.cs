namespace Application.DTO
{
    public class SearchDTO
    {
        public string[] searchFields { get; set; }
        public string[] searchValues { get; set; }
        public string sortField { get; set; }
        public bool sortAscending { get; set; }
        public required int pageSize { get; set; }
        public required int skip { get; set; }
    }
    
}
