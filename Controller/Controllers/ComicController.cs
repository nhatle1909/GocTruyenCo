using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComicController : ControllerBase
    {
      private readonly IComicService _comicService;
      public ComicController(IComicService comicService)
        {
            _comicService = comicService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComicAsync(CommandComicDTO comicDTO)
        {
            var query = await _comicService.CreateComicAsync(comicDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpPut("{comicId}")]
        public async Task<IActionResult> UpdateComicAsync(Guid id, CommandComicDTO comicDTO)
        {
            var query = await _comicService.UpdateComicAsync(id, comicDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpDelete("{comicId}")]
        public async Task<IActionResult> DeleteComicAsync(Guid id)
        {
            var query = await _comicService.DeleteComicAsync(id);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpGet("{comicId}")]
        public async Task<IActionResult> GetComicAsync(Guid id)
        {
            var query = await _comicService.GetComicAsync(id);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpGet]
        public async Task<IActionResult> GetComicPagingAsync([FromQuery] SearchDTO searchDTO)
        {
            var query = await _comicService.GetComicPagingAsync(searchDTO);
            return Ok(query);
        }
    }
}
