using Application.Aggregation;
using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class ComicChapterCommentService : IComicChapterCommentService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicChapterCommentService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> CreateNewComment(CommandComicChapterCommentDTO commandComicChapterCommentDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var comment = _mapper.Map<ComicChapterComment>(commandComicChapterCommentDTO);
                bool command = await _unitofwork.GetRepository<ComicChapterComment>().AddOneItemAsync(comment);
                if (!command)
                {
                    result.CustomResponse(false, false, "Create new comment failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Create new comment successfully");

            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryComicChapterCommentDTO>>> GetPagingAsync(SearchDTO searchDTO)
        {
            ServiceResponse<IEnumerable<QueryComicChapterCommentDTO>> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ComicChapterComment>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField,
                                                                                               searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip, ComicChapterCommentAggregation.ComicChapterCommentBsonAggregation);
                var queryDTO = _mapper.Map<IEnumerable<QueryComicChapterCommentDTO>>(query);
                result.CustomResponse(queryDTO, true, "Get comments successfully");
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
                var query = await _unitofwork.GetRepository<ComicChapterComment>().CountAsync(countDTO.searchFields, countDTO.searchValues, countDTO.pageSize);
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
