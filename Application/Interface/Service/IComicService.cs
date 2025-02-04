using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IComicService
    {
        Task<ServiceResponse<bool>> CreateComicAsync(CommandComicDTO comicDTO);
        Task<ServiceResponse<bool>> UpdateComicAsync(Guid id, CommandComicDTO comicDTO);
        Task<ServiceResponse<bool>> DeleteComicAsync(Guid id);
        Task<ServiceResponse<QueryComicDTO>> GetComicAsync(Guid id);
        Task<ServiceResponse<List<QueryComicDTO>>> GetComicPagingAsync(SearchDTO searchDTO);
        Task<ServiceResponse<bool>> UpdateComicStatusAsync(Guid id, string status);
        Task<ServiceResponse<bool>> UpdateComicChapterNumberAsync(Guid id, int chapters);


    }
}
