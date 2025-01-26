using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ComicCategoryService : IComicCategoryService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicCategoryService(IUnitofwork unitofwork,IMapper mapper ) 
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateComicCategoryAsync(CommandComicCategoryDTO createComicCategoryDTO)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            
            var newComicCategory = _mapper.Map<ComicCategory>(createComicCategoryDTO);

            var command = await _unitofwork.GetRepository<ComicCategory>().AddOneItemAsync(newComicCategory);
            
            if (command != null)
            {
                await _unitofwork.CommitAsync();
                result.SuccessCreateResponse();
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteComicCategoryAsync(Guid id)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();

            var query = await _unitofwork.GetRepository<ComicCategory>().GetByIdAsync(id);
            if (query != null)
            {
                query.isDeleted = true;
                await _unitofwork.GetRepository<ComicCategory>().UpdateItemAsync(id,query);
                await _unitofwork.CommitAsync();
                result.SuccessDeleteResponse();
            }
            return result;
        }

        public async Task<ServiceResponse<List<QueryComicCategoryDTO>>> GetComicCategoriesAsync(bool isHentai)
        {
            ServiceResponse<List<QueryComicCategoryDTO>> result = new ServiceResponse<List<QueryComicCategoryDTO>>();

            var query = await _unitofwork.GetRepository<ComicCategory>().GetAllAsync();
            if (query != null)
            {
                var queryDTO = _mapper.Map<List<QueryComicCategoryDTO>>(query);
                result.SuccessRetrieveResponse(queryDTO);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateComicCategoryAsync(Guid comicCategoryId,CommandComicCategoryDTO updateComicCategoryDTO)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            var newComicCategory = _mapper.Map<ComicCategory>(updateComicCategoryDTO);
            
            var query = await _unitofwork.GetRepository<ComicCategory>().UpdateItemAsync(comicCategoryId, newComicCategory);

            if (query)
            {
                await _unitofwork.CommitAsync();
                result.SuccessUpdateResponse();
            }       
            return result;
        }
    }
}
