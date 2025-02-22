using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class ForumTopicCategoryService : IForumTopicCategoryService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ForumTopicCategoryService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> CreateNewTopicCategory(CommandForumTopicCategoryDTO commandForumTopicCategory)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var item = _mapper.Map<ForumTopicCategory>(commandForumTopicCategory);
                bool command = await _unitofwork.GetRepository<ForumTopicCategory>().AddOneItemAsync(item);
                if (!command)
                {
                    result.CustomResponse(false, false, "Add New forum category failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Add new forum category successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;

        }

        public async Task<ServiceResponse<bool>> DeleteCategory(Guid topicCategoryId)
        {
            ServiceResponse<bool> result = new();
            try
            {
                bool command = await _unitofwork.GetRepository<ForumTopicCategory>().RemoveItemAsync(topicCategoryId);
                if (!command)
                {
                    result.CustomResponse(false, false, "Delete category failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Delete category successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryForumTopicCategoryDTO>>> GetAllTopics()
        {
            ServiceResponse<IEnumerable<QueryForumTopicCategoryDTO>> result = new();
            try
            {
                //var query = await _unitofwork.GetRepository<QueryForumTopicCategoryDTO>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField,
                //                                                                                   searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip);
                var query = await _unitofwork.GetRepository<ForumTopicCategory>().GetAllAsync();
                var queryDTO = _mapper.Map<IEnumerable<QueryForumTopicCategoryDTO>>(query);
                await _unitofwork.CommitAsync();
                result.CustomResponse(queryDTO, true, "Retrieve data successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
