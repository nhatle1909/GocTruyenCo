using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ComicCategoryController : ControllerBase
    {
        private readonly IComicCategoryService _comicCategoryService;
        public ComicCategoryController(IComicCategoryService comicCategoryService)
        {
            _comicCategoryService = comicCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAction(bool isHentai)
        {
            var query = await _comicCategoryService.GetComicCategoriesAsync(isHentai);
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostAction(CommandComicCategoryDTO createComicCategoryDTO)
        {
            var query = await _comicCategoryService.CreateComicCategoryAsync(createComicCategoryDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpPut("{comicCategoryId}")]
        public async Task<IActionResult> PutAction(Guid comicCategoryId, CommandComicCategoryDTO updateComicCategoryDTO)
        {
            var query = await _comicCategoryService.UpdateComicCategoryAsync(comicCategoryId, updateComicCategoryDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpDelete("{comicCategoryId}")]
        public async Task<IActionResult> DeleteAction(Guid comicCategoryId)
        {
            var query = await _comicCategoryService.DeleteComicCategoryAsync(comicCategoryId);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
    }
}
