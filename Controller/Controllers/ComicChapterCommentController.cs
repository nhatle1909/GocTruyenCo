using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Controller.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ComicChapterCommentController : ControllerBase
    {
        private readonly IComicChapterCommentService _comicChapterCommentService;


        public ComicChapterCommentController(IComicChapterCommentService comicChapterCommentService)
        {
            _comicChapterCommentService = comicChapterCommentService;
        }

        [HttpGet()]
        public async Task<IActionResult> PagingAsync([FromQuery] SearchDTO searchDTO)
        {
            var query = await _comicChapterCommentService.GetPagingAsync(searchDTO);
            return Ok(query);
        }


        [Authorize(Roles = "Reader")]
        [HttpPost()]
        public async Task<IActionResult> Create(CommandComicChapterCommentDTO commandComicChapterCommentDTO)
        {
            var query = await _comicChapterCommentService.CreateNewComment(commandComicChapterCommentDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> Count([FromQuery] CountDTO countDTO)
        {
            var query = await _comicChapterCommentService.CountAsync(countDTO);
            return Ok(query);
        }
    }
}
