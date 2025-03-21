using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IComicChapterService
    {
        Task<ServiceResponse<IEnumerable<QueryComicChapterDTO>>> GetAllChapter(Guid comicId);
        Task<ServiceResponse<IEnumerable<QueryComicChapterDTO>>> GetChaptersPaging(SearchDTO searchDTO);
        Task<ServiceResponse<bool>> UpdateChapter(Guid chapterId, CommandComicChapterDTO commandComicChapterDTO);
        Task<ServiceResponse<string>> CreateChapter(CommandComicChapterDTO commandComicChapterDTO);
        Task<ServiceResponse<long>> CountAsync(CountDTO countDTO);
    }
}
