using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IForumTopicService
    {
        Task<ServiceResponse<bool>> CreateNewTopic(CommandForumTopicDTO commandForumTopic);
        Task<ServiceResponse<bool>> DeleteTopic(Guid topicId);
        Task<ServiceResponse<bool>> UpdateTopicLock(Guid topicId, bool islock);
        Task<ServiceResponse<IEnumerable<QueryForumTopicDTO>>> GetPagingAsync(SearchDTO searchDTO);
        Task<ServiceResponse<long>> CountAsync(CountDTO countDTO);
    }
}
