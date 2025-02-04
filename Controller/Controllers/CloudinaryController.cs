using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CloudinaryController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;
        public CloudinaryController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("Theme")]
        public async Task<IActionResult> UploadThemeUrl(IFormFile imageFile)
        {
            var response = await _cloudinaryService.UploadThemeUrl("imageDTO.ComicName", imageFile);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("Chapter")]
        public async Task<IActionResult> UploadChapterImage(string comicName, string chapterNumber, List<IFormFile> imageURIs)
        {
            var response = await _cloudinaryService.UploadChapterImage(comicName, chapterNumber, imageURIs);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
