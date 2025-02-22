using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IForumTopicCommentService
    {
        Task<ServiceResponse<bool>> PostNewComment(CommandForumTopicCommentDTO commandForumTopicCommentDTO);
        Task<ServiceResponse<IEnumerable<QueryForumTopicCommentDTO>>> GetPagingAsync(SearchDTO searchDTO);
        Task<ServiceResponse<long>> CountAsync(CountDTO countDTO);
    }
}
