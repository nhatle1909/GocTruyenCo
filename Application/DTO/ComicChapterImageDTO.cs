using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class QueryComicChapterImageDTO
    {
        public required Guid Id { get; set; }
        public required Guid ComicChapterId { get; set; }
        public required string ImageUrl { get; set; }
    }
    public class CommandComicChapterImageDTO
    {
       
        public required Guid ComicChapterId { get; set; }
        public required string ImageUrl { get; set; }
    }
}
