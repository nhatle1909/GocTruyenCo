using Application.Aggregation;
using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ForumTopicService : IForumTopicService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ForumTopicService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> CreateNewTopic(CommandForumTopicDTO commandForumTopic)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var item = _mapper.Map<ForumTopic>(commandForumTopic);
                bool command = await _unitofwork.GetRepository<ForumTopic>().AddOneItemAsync(item);
                if (!command)
                {
                    result.CustomResponse(false, false, "Create topic failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Create topic successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteTopic(Guid topicId)
        {
            ServiceResponse<bool> result = new();
            try
            {
                bool command = await _unitofwork.GetRepository<ForumTopic>().RemoveItemAsync(topicId);
                if (!command) 
                {
                    result.CustomResponse(false, false, "Delete topic failed"); 
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Delete topic successful");
            }
            catch(Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryForumTopicDTO>>> GetPagingAsync(SearchDTO searchDTO)
        {
            ServiceResponse<IEnumerable<QueryForumTopicDTO>> result = new();
            try 
            {
                var query = await _unitofwork.GetRepository<ForumTopic>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField,
                                                                                     searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip,ForumTopicAggregation.ForumTopicBsonAggregation);
                var queryDTO = _mapper.Map<IEnumerable<QueryForumTopicDTO>>(query);
                
                await _unitofwork.CommitAsync();
                result.CustomResponse(queryDTO, true, "Retrieve data successful");
            }
            catch(Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateTopicLock(Guid topicId,bool isLock)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ForumTopic>().GetByIdAsync(topicId);
                if (query == null)
                {
                    result.CustomResponse(false, false, "Topic not found");
                    return result;
                }
                query.isLock = isLock;
                bool command = await _unitofwork.GetRepository<ForumTopic>().UpdateItemAsync(topicId, query);
                if(!command)
                {
                    result.CustomResponse(false, false, "Update topic failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Update topic successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
