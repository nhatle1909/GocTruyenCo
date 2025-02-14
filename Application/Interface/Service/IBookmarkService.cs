using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IBookmarkService
    {
        Task<ServiceResponse<bool>> AddBookmark(CommandBookmarkDTO commandBookmarkDTO);
        Task<ServiceResponse<bool>> DeleteBookmark(Guid bookmarkId);
        Task<ServiceResponse<bool>> UpdateBookmarkType(Guid bookmarkId, string bookmarkType);
        Task<ServiceResponse<IEnumerable<QueryBookmarkDTO>>> GetPagingAsync(SearchDTO searchDTO);

    }
}
