using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class QueryComicChapterDTO
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
    public class CommandComicChapterDTO
    {
        public required string Name { get; set; }
    }
}
