using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin,Uploader")]
        [HttpPost("{comicName}")]
        public async Task<IActionResult> UploadThemeUrl(string comicName, IFormFile imageFile)
        {
            var response = await _cloudinaryService.UploadThemeUrl(comicName, imageFile);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("{comicName},{chapterNumber}")]
        public async Task<IActionResult> UploadChapterImage(string comicName, string chapterNumber, List<IFormFile> imageURIs)
        {
            var response = await _cloudinaryService.UploadChapterImage(comicName, chapterNumber, imageURIs);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
