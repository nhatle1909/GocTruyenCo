using Application.Common;
using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IComicChapterService
    {
        Task<ServiceResponse<IEnumerable<QueryComicChapterDTO>>> GetAllChapter(Guid comicId);
        Task<ServiceResponse<IEnumerable<QueryComicChapterDTO>>> GetChaptersPaging(SearchDTO searchDTO);
        Task<ServiceResponse<bool>> UpdateChapter(Guid chapterId,CommandComicChapterDTO commandComicChapterDTO);
        Task<ServiceResponse<bool>> CreateChapter(CommandComicChapterDTO commandComicChapterDTO);
    }
}
