using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IForumTopicCategoryService
    {
        Task<ServiceResponse<bool>> CreateNewTopicCategory(CommandForumTopicCategoryDTO commandForumTopicCategory);
        Task<ServiceResponse<IEnumerable<QueryForumTopicCategoryDTO>>> GetAllTopics();
        Task<ServiceResponse<bool>> DeleteCategory(Guid topicCategoryId);
    }
}
