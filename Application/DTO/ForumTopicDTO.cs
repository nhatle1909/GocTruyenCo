using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
     public class QueryForumTopicDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CreatorId { get; set; }
        public string CreatedDate { get; set; }
        public Guid ForumCategoryId { get; set; }
        public bool isLock { get; set; }
    }
    public class CommandForumTopicDTO
    {
        public Guid CreatorId { get; set; }
        public string Title { get; set; }
        public Guid ForumCategoryId { get; set; }
    }
}
