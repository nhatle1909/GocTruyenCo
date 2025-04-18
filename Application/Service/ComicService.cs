﻿using Application.Aggregation;
using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class ComicService : IComicService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateComicAsync(CommandComicDTO comicDTO)
        {
            ServiceResponse<bool> result = new();


            try
            {
                var comic = _mapper.Map<Comic>(comicDTO);
                var command = await _unitofwork.GetRepository<Comic>().AddOneItemAsync(comic);

                if (!command)
                {
                    result.CustomResponse(false, false, "Create comic failed");
                    return result;

                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Create comic successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteComicAsync(Guid id)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<Comic>().GetByIdAsync(id);
                if (query == null)
                {
                    result.CustomResponse(false, false, "Comic not found");

                }

                query.isDeleted = true;
                await _unitofwork.GetRepository<Comic>().UpdateItemAsync(id, query);
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Delete comic successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<QueryComicDTO>> GetComicAsync(Guid id)
        {
            ServiceResponse<QueryComicDTO> result = new();

            try
            {
                var query = await _unitofwork.GetRepository<Comic>().GetByIdAsync(id, ComicAggregation.ComicBsonAggregation);

                if (query == null)
                {
                    result.CustomResponse(null, false, "Comic not found");
                }
                var queryDTO = _mapper.Map<QueryComicDTO>(query);
                result.CustomResponse(queryDTO, true, "Retrieve data successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<List<QueryComicDTO>>> GetComicPagingAsync(SearchDTO searchDTO)
        {
            ServiceResponse<List<QueryComicDTO>> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<Comic>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField
                    , searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip, ComicAggregation.ComicBsonAggregation);

                var queryDTO = _mapper.Map<List<QueryComicDTO>>(query);

                result.CustomResponse(queryDTO, true, "Retrieve data successful");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateComicAsync(Guid id, CommandComicDTO comicDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var comic = _mapper.Map<Comic>(comicDTO);
                comic.Id = id;
                var command = await _unitofwork.GetRepository<Comic>().UpdateItemAsync(id, comic);

                if (!command)
                {
                    result.CustomResponse(false, false, "Update comic failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Update comic successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
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
        public async Task<ServiceResponse<long>> CountAsync(CountDTO countDTO)
        {
            ServiceResponse<long> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<Comic>().CountAsync(countDTO.searchFields, countDTO.searchValues, countDTO.pageSize);
                result.CustomResponse(query, true, "Count successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
