using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class QueryForumTopicCommentDTO
    {

        public string AccountName { get; set; }
        public string CreatedDate { get; set; }
        public string Comment { get; set; }
    }
    public class CommandForumTopicCommentDTO
    {
        public Guid AccountId { get; set; }
        public string Comment { get; set; }
    }
}
