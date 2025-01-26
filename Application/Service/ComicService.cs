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
    public class ComicService : IComicService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicService(IUnitofwork unitofwork,IMapper mapper) 
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateComicAsync(CommandComicDTO comicDTO)
        {
            ServiceResponse<bool> result = new();
            
            var comic = _mapper.Map<Comic>(comicDTO);

            var query = await _unitofwork.GetRepository<Comic>().AddOneItemAsync(comic);

            if (query)
            {
                await _unitofwork.CommitAsync();
                result.SuccessCreateResponse();
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteComicAsync(Guid id)
        {
            ServiceResponse<bool> result = new();

            var query = await _unitofwork.GetRepository<Comic>().GetByIdAsync(id);
            if (query != null)
            {
                query.isDeleted = true;
                await _unitofwork.GetRepository<Comic>().UpdateItemAsync(id,query);
                await _unitofwork.CommitAsync();
                result.SuccessDeleteResponse();
            }
            return result;
        }

        public async Task<ServiceResponse<QueryComicDTO>> GetComicAsync(Guid id)
        {
            ServiceResponse<QueryComicDTO> result = new();

            var query = await _unitofwork.GetRepository<Comic>().GetByIdAsync(id);

            if (query != null)
            {
                var queryDTO =  _mapper.Map<QueryComicDTO>(query);
                result.SuccessRetrieveResponse(queryDTO);
            }

            return result;
        }

        public async Task<ServiceResponse<List<QueryComicDTO>>> GetComicPagingAsync(SearchDTO searchDTO)
        {
           ServiceResponse<List<QueryComicDTO>> result = new();
            var query = await _unitofwork.GetRepository<Comic>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField
                , searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip);

            var queryDTO = _mapper.Map<List<QueryComicDTO>>(query);

            if (queryDTO.Count > 0) result.SuccessRetrieveResponse(queryDTO);

            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateComicAsync(Guid id, CommandComicDTO comicDTO)
        {
            ServiceResponse<bool> result = new();

            var comic = _mapper.Map<Comic>(comicDTO);

            var query = await _unitofwork.GetRepository<Comic>().UpdateItemAsync(id,comic);

            if (query)
            {
                await _unitofwork.CommitAsync();
                result.SuccessUpdateResponse();
            }
            return result;
        }

        public Task<ServiceResponse<bool>> UpdateComicChapterNumberAsync(Guid id, int chapters)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> UpdateComicStatusAsync(Guid id, string status)
        {
            throw new NotImplementedException();
        }
    }
}
