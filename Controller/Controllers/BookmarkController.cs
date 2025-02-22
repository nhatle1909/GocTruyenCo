using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;
namespace Controller.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;



        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;

        }

        [HttpGet()]
        public async Task<IActionResult> PagingAsync([FromQuery] SearchDTO searchDTO)
        {
            var query = await _bookmarkService.GetPagingAsync(searchDTO);
            return Ok(query);
        }
        [HttpPost()]
        public async Task<IActionResult> AddBookmark(CommandBookmarkDTO commandBookmarkDTO)
        {
            var command = await _bookmarkService.AddBookmark(commandBookmarkDTO);
            if (!command.Success)
            {
                return BadRequest(command);
            }
            return Ok(command);
        }
        [HttpDelete("{BookmarkId}")]
        public async Task<IActionResult> Delete(Guid BookmarkId)
        {
            var query = await _bookmarkService.DeleteBookmark(BookmarkId);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpPut("{BookmarkId}")]
        public async Task<IActionResult> Put(Guid BookmarkId, string bookmarkType)
        {

            var query = await _bookmarkService.UpdateBookmarkType(BookmarkId, bookmarkType);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpGet("Count")]
        public async Task<IActionResult> Count([FromQuery] CountDTO countDTO)
        {
            var query = await _bookmarkService.CountAsync(countDTO);
            return Ok(query);
        }
    }
}
