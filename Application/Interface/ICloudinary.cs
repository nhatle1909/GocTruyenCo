using Microsoft.AspNetCore.Http;

namespace Application.Interface
{
    public interface ICloudinaryRepository
    {
        Task<IEnumerable<string>> UploadChapterImageAsync(string comicName, string chapterNumber, List<IFormFile> imageURIs);
        Task<string> UploadComicThemeAsync(string ImageUri, IFormFile image);
    }
}
