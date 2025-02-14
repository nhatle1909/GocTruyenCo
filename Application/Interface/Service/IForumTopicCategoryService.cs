using Application.Common;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IForumTopicCategoryService
    {
        Task<ServiceResponse<bool>> CreateNewTopicCategory(CommandForumTopicCategoryDTO commandForumTopicCategory);
        Task<ServiceResponse<IEnumerable<QueryForumTopicCategoryDTO>>> GetAllTopics();
        Task<ServiceResponse<bool>> DeleteCategory(Guid topicCategoryId);
    }
}
