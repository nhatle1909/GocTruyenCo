using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IComicChapterImageService
    {
        Task<ServiceResponse<IEnumerable<QueryComicChapterImageDTO>>> GetAllChapterImage(Guid comicChapterId);
        Task<ServiceResponse<bool>> CreateChapterImagesAsync(Guid comicChapterId,List<CommandComicChapterImageDTO> createChapterImageDTO);
        Task<ServiceResponse<bool>> UpdateChapterImagesAsync(Guid comicChapterId, List<CommandComicChapterImageDTO> updateChapterImageDTO);
        Task<ServiceResponse<bool>> DeleteChapterImageAsync(Guid id);
    }
}
