using Application.Aggregation;
using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class ForumTopicCommentService : IForumTopicCommentService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ForumTopicCommentService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IEnumerable<QueryForumTopicCommentDTO>>> GetPagingAsync(SearchDTO searchDTO)
        {
            ServiceResponse<IEnumerable<QueryForumTopicCommentDTO>> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ForumTopicComment>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField,
                                                                                             searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip, ForumTopicCommentAggregation.ForumTopicCommentBsonAggregation);
                var queryDTO = _mapper.Map<IEnumerable<QueryForumTopicCommentDTO>>(query);
                await _unitofwork.CommitAsync();
                result.CustomResponse(queryDTO, true, "Retrieve data successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> PostNewComment(CommandForumTopicCommentDTO commandForumTopicCommentDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var item = _mapper.Map<ForumTopicComment>(commandForumTopicCommentDTO);
                bool command = await _unitofwork.GetRepository<ForumTopicComment>().AddOneItemAsync(item);
                if (!command)
                {
                    result.CustomResponse(false, false, "Post new comment failed");
                    return result;

                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Post new comment successful");

            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
        public async Task<ServiceResponse<long>> CountAsync(CountDTO countDTO)
        {
            ServiceResponse<long> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ForumTopicComment>().CountAsync(countDTO.searchFields, countDTO.searchValues, countDTO.pageSize);
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
