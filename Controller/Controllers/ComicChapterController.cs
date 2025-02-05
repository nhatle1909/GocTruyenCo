using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComicChapterController : ControllerBase
    {
        private readonly IComicChapterService _comicChapterService;
        public ComicChapterController(IComicChapterService comicChapterService)
        {
            _comicChapterService = comicChapterService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComicAsync(CommandComicChapterDTO commandComicChapterDTO)
        {
            var query = await _comicChapterService.CreateChapter(commandComicChapterDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpGet("{comicId}")]
        public async Task<IActionResult> GetComicChapterAsync(Guid comicId)
        {
            var query = await _comicChapterService.GetAllChapter(comicId);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaging(SearchDTO searchDTO)
        {
            var query = await _comicChapterService.GetChaptersPaging(searchDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
    }
}
