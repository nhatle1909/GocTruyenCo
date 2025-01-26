using Application.Common;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IComicCategoryService
    {
        Task<ServiceResponse<bool>> CreateComicCategoryAsync(CommandComicCategoryDTO createComicCategoryDTO);
        Task<ServiceResponse<bool>> UpdateComicCategoryAsync(Guid comicCategoryId,CommandComicCategoryDTO updateComicCategoryDTO);
        Task<ServiceResponse<bool>> DeleteComicCategoryAsync(Guid id);
        //Task<ComicCategoryDTO> GetComicCategoryByIdAsync(Guid id);
        Task<ServiceResponse<List<QueryComicCategoryDTO>>> GetComicCategoriesAsync(bool isHentai);
    }
}
