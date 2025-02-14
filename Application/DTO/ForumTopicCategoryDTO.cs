using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class QueryForumTopicCategoryDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }
    public class CommandForumTopicCategoryDTO
    {
        public string CategoryName { get; set; }
    }
}
