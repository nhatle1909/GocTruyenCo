using Application.Common;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IComicChapterImageService
    {
        Task<ServiceResponse<IEnumerable<QueryAccountDTO>>> GetAllChapterImage(Guid comicChapterId);
        Task<ServiceResponse<bool>> CreateChapterImagesAsync(List<CommandComicChapterImageDTO> createChapterImageDTO);
        Task<ServiceResponse<bool>> UpdateChapterImagesAsync(List<CommandComicChapterImageDTO> updateChapterImageDTO);
        Task<ServiceResponse<bool>> DeleteChapterImageAsync(Guid id);
    }
}
