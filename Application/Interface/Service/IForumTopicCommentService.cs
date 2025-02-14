using Application.Common;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IForumTopicCommentService
    {
        Task<ServiceResponse<bool>> PostNewComment(CommandForumTopicCommentDTO commandForumTopicCommentDTO);
        Task<ServiceResponse<IEnumerable<QueryForumTopicCommentDTO>>> GetPagingAsync(SearchDTO searchDTO);
    }
}
