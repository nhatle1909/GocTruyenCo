using Application.Common;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IComicChapterCommentService
    {
        Task<ServiceResponse<bool>> CreateNewComment (CommandComicChapterCommentDTO commandComicChapterCommentDTO);
        Task<ServiceResponse<QueryComicChapterCommentDTO>> GetPagingAsync(SearchDTO searchDTO);
    }
}
