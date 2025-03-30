using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class ComicChapterImageController : ControllerBase
    {
        private readonly IComicChapterImageService _comicChapterImageService;
        public ComicChapterImageController(IComicChapterImageService comicChapterImageService)
        {
            _comicChapterImageService = comicChapterImageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComicChapterImagesAsync(Guid comicChapterId, List<CommandComicChapterImageDTO> commandComicChapterImageDTOs)
        {
            var query = await _comicChapterImageService.CreateChapterImagesAsync(comicChapterId, commandComicChapterImageDTOs);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpGet("{comicChapterId}")]
        public async Task<IActionResult> GetComicChapterAsync(Guid comicChapterId)
        {
            var query = await _comicChapterImageService.GetAllChapterImage(comicChapterId);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpPut("{comicChapterId}")]
        public async Task<IActionResult> UpdateChapterImagesAsync(Guid comicChapterId, List<CommandComicChapterImageDTO> commandComicChapterImageDTOs)
        {
            var query = await _comicChapterImageService.UpdateChapterImagesAsync(comicChapterId, commandComicChapterImageDTOs);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
    }
}
