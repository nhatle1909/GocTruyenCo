using Application.Interface;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;

namespace Infrastructure
{
    public class CloudinaryRepository : ICloudinaryRepository
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryRepository(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadComicThemeAsync(string comicName, IFormFile image)
        {
            var comic = await CheckIfComicExist(comicName);
            try
            {
                await using var stream = image.OpenReadStream();

                var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                {
                    File = new FileDescription(image.FileName, stream),
                    PublicId = comic,
                    Folder = comic,
                    Overwrite = true,

                };
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                return uploadResult.Url.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<string>> UploadChapterImageAsync(string comicName, string chapterNumber, List<IFormFile> images)
        {
            List<string> imageUrls = new();
            if (images.Count == 0) return null;
            try
            {
                var chapter = await CheckIfChapterExist(comicName, chapterNumber);

                foreach (var image in images)
                {
                    var imageIndex = images.FindIndex(a => a.FileName.Equals(image.FileName));

                    await using var stream = image.OpenReadStream();

                    var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, stream),
                        PublicId = "Image" + imageIndex,
                        Overwrite = true,
                        Folder = chapter,

                    };
                    var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    imageUrls.Add(uploadResult.Url.ToString());
                }
                return imageUrls;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task<string> CheckIfComicExist(string comicName)
        {
            var folderList = _cloudinary.Search().Expression($"folder:{comicName}").Execute();
            if (folderList.TotalCount == 0)
            {
                await _cloudinary.CreateFolderAsync(comicName);
            }
            return comicName;
        }
        private async Task<string> CheckIfChapterExist(string comicName, string chapterNumber)
        {
            var folderList = _cloudinary.Search().Expression($"folder:{comicName}/{chapterNumber}").Execute();
            if (folderList.TotalCount == 0)
            {
                await _cloudinary.CreateFolderAsync($"{comicName}/{chapterNumber}");
            }
            return $"{comicName}/{chapterNumber}";
        }
    }
}
