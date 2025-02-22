using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
namespace Controller.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ForumTopicCategoryController : ControllerBase
    {
        private readonly IForumTopicCategoryService _forumTopicCategoryService;

        public ForumTopicCategoryController(IForumTopicCategoryService forumTopicCategoryService)
        {
            _forumTopicCategoryService = forumTopicCategoryService;

        }

        [HttpGet()]
        public async Task<IActionResult> PagingAsync()
        {
            var query = await _forumTopicCategoryService.GetAllTopics();
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewTopicCategory(CommandForumTopicCategoryDTO commandForumTopicCategoryDTO)
        {
            var command = await _forumTopicCategoryService.CreateNewTopicCategory(commandForumTopicCategoryDTO);
            if (!command.Success)
            {
                return BadRequest(command);
            }
            return Ok(command);
        }

        [HttpDelete("{topicCategoryId}")]
        public async Task<IActionResult> DeleteTopic(Guid topicCategoryId)
        {
            var command = await _forumTopicCategoryService.DeleteCategory(topicCategoryId);
            if (!command.Success)
            {
                return BadRequest(command);
            }
            return Ok(command);
        }
    }
}
