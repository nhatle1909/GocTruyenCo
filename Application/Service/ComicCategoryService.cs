using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class ComicCategoryService : IComicCategoryService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicCategoryService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateComicCategoryAsync(CommandComicCategoryDTO createComicCategoryDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var newComicCategory = _mapper.Map<ComicCategory>(createComicCategoryDTO);

                var command = await _unitofwork.GetRepository<ComicCategory>().AddOneItemAsync(newComicCategory);

                if (!command)
                {
                    result.CustomResponse(false, false, "Create comic category failed");
                    return result;
                }
             
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Create comic category successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteComicCategoryAsync(Guid id)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ComicCategory>().GetByIdAsync(id);
                if (query == null)
                {
                    result.CustomResponse(false, false, "Comic category not found");
                    return result;
                }
             
                query.isDeleted = true;
                await _unitofwork.GetRepository<ComicCategory>().UpdateItemAsync(id, query);
                await _unitofwork.CommitAsync();

                result.CustomResponse(true, true, "Delete comic category successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<List<QueryComicCategoryDTO>>> GetComicCategoriesAsync(bool isHentai)
        {
            ServiceResponse<List<QueryComicCategoryDTO>> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ComicCategory>().GetAllAsync();
                
                var queryDTO = _mapper.Map<List<QueryComicCategoryDTO>>(query);
                result.CustomResponse(queryDTO, true, "Retrieve data successful");
                
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateComicCategoryAsync(Guid comicCategoryId, CommandComicCategoryDTO updateComicCategoryDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var newComicCategory = _mapper.Map<ComicCategory>(updateComicCategoryDTO);
                newComicCategory.Id = comicCategoryId;
                var command = await _unitofwork.GetRepository<ComicCategory>().UpdateItemAsync(comicCategoryId, newComicCategory);

                if (!command)
                {
                    result.CustomResponse(false, false, "Update comic category failed");
                    return result;
                }
                  
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Update comic category successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
