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

                if (command)
                {
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Create comic category successful");
                }
                else
                {
                    result.CustomResponse(false, false, "Create comic category failed");
                }
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
                if (query != null)
                {
                    query.isDeleted = true;
                    await _unitofwork.GetRepository<ComicCategory>().UpdateItemAsync(id, query);
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Delete comic category successful");
                }
                else
                {
                    result.CustomResponse(false, false, "Comic category not found");
                }
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
                if (query != null)
                {
                    var queryDTO = _mapper.Map<List<QueryComicCategoryDTO>>(query);
                    result.CustomResponse(queryDTO, true, "Retrieve data successful");
                }
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

                var query = await _unitofwork.GetRepository<ComicCategory>().UpdateItemAsync(comicCategoryId, newComicCategory);

                if (query)
                {
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Update comic category successful");
                }
                else
                {
                    result.CustomResponse(false, false, "Update comic category failed");
                }
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
