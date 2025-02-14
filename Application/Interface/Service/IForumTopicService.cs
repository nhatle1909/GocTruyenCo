using Application.Common;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IForumTopicService
    {
        Task<ServiceResponse<bool>> CreateNewTopic(CommandForumTopicDTO commandForumTopic);
        Task<ServiceResponse<bool>> DeleteTopic(Guid topicId);
        Task<ServiceResponse<bool>> UpdateTopicLock(Guid topicId, bool islock);
        Task<ServiceResponse<IEnumerable<QueryForumTopicDTO>>> GetPagingAsync(SearchDTO searchDTO);

    }
}
