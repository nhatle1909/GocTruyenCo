using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
namespace Controller.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ForumTopicController : ControllerBase
    {
        private readonly IForumTopicService _forumTopicService;

        public ForumTopicController(IForumTopicService forumTopicService)
        {
            _forumTopicService = forumTopicService;

        }

        [HttpGet()]
        public async Task<IActionResult> PagingAsync([FromQuery] SearchDTO searchDTO)
        {
            var query = await _forumTopicService.GetPagingAsync(searchDTO);
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostNewTopic(CommandForumTopicDTO commandForumTopicDTO)
        {
            var command = await _forumTopicService.CreateNewTopic(commandForumTopicDTO);
            if (!command.Success)
            {
                return BadRequest(command);
            }
            return Ok(command);
        }
        [HttpPut("{topicId}")]
        public async Task<IActionResult> UpdateTopic(Guid topicId, bool isLock)
        {
            var command = await _forumTopicService.UpdateTopicLock(topicId, isLock);
            if (!command.Success)
            {
                return BadRequest(command);
            }
            return Ok(command);
        }
        [HttpDelete("{topicId}")]
        public async Task<IActionResult> DeleteTopic(Guid topicId)
        {
            var command = await _forumTopicService.DeleteTopic(topicId);
            if (!command.Success)
            {
                return BadRequest(command);
            }
            return Ok(command);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> Count([FromQuery] CountDTO countDTO)
        {
            var query = await _forumTopicService.CountAsync(countDTO);
            return Ok(query);
        }
    }
}
