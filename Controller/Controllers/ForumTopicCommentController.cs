using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForumTopicCommentController : ControllerBase
    {
        private readonly IForumTopicCommentService _forumTopicCommentService;

        public ForumTopicCommentController(IForumTopicCommentService forumTopicCommentService)
        {
            _forumTopicCommentService = forumTopicCommentService;
          
        }

        [HttpGet()]
        public async Task<IActionResult> PagingAsync([FromQuery] SearchDTO searchDTO)
        {
            var query = await _forumTopicCommentService.GetPagingAsync(searchDTO);
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostNewTopic(CommandForumTopicCommentDTO commandForumTopicCommentDTO)
        {
            var command = await _forumTopicCommentService.PostNewComment(commandForumTopicCommentDTO);
            if (!command.Success)
            {
                return BadRequest(command);
            }
            return Ok(command);
        }
    }
}
