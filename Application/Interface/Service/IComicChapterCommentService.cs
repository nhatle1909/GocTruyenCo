using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IComicChapterCommentService
    {
        Task<ServiceResponse<bool>> CreateNewComment(CommandComicChapterCommentDTO commandComicChapterCommentDTO);
        Task<ServiceResponse<IEnumerable<QueryComicChapterCommentDTO>>> GetPagingAsync(SearchDTO searchDTO);
        Task<ServiceResponse<long>> CountAsync(CountDTO countDTO);
    }
}
