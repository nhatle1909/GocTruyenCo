using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class ComicChapterImageService : IComicChapterImageService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicChapterImageService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateChapterImagesAsync(List<CommandComicChapterImageDTO> createChapterImageDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var items = _mapper.Map<IEnumerable<ComicChapterImage>>(createChapterImageDTO);
                var itemList = items.ToList();
                bool command = await _unitofwork.GetRepository<ComicChapterImage>().AddManyItemAsync(itemList);
                //foreach (var item in createChapterImageDTO)
                //{
                //    var comicChapterImage = _mapper.Map<ComicChapterImage>(item);
                //    bool command = await _unitofwork.GetRepository<ComicChapterImage>().AddOneItemAsync(comicChapterImage);
                //}
                if (!command)
                {
                    result.CustomResponse(false, false, "Add chapter image failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Create chapter image successfully");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteChapterImageAsync(Guid id)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ComicChapterImage>().GetByIdAsync(id);
                if (query == null)
                {
                    result.CustomResponse(false, false, "Chapter image not found");
                }

                query.isDeleted = true;
                await _unitofwork.GetRepository<ComicChapterImage>().UpdateItemAsync(id, query);
                await _unitofwork.CommitAsync();

                result.CustomResponse(true, true, "Delete chapter image successfully");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryComicChapterImageDTO>>> GetAllChapterImage(Guid comicChapterId)
        {
            ServiceResponse<IEnumerable<QueryComicChapterImageDTO>> result = new();
            try
            {
                string[] searchField = ["ComicChapterId"];
                string[] searchValue = [comicChapterId.ToString()];
                var query = await _unitofwork.GetRepository<ComicChapterImage>().GetAllByFilterAsync(searchField, searchValue);

                var data = _mapper.Map<IEnumerable<QueryComicChapterImageDTO>>(query);
                result.CustomResponse(data, true, "Get all chapter image successfully");

            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        //Do i need to update chapter image? if Url of image is not changed and the only thing updated is image of that url ?
        public async Task<ServiceResponse<bool>> UpdateChapterImagesAsync(Guid comicChapterId, List<CommandComicChapterImageDTO> updateChapterImageDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                //Find all chapter image of a chapter
                string[] searchFields = ["ComicChapterId"];
                string[] searchValues = [comicChapterId.ToString()];
                var comicImages = await _unitofwork.GetRepository<ComicChapterImage>().GetAllByFilterAsync(searchFields, searchValues);
                // Sort images by order of URL ( ../Image01.jpg, ../Image02.jpg, ...)
                comicImages.OrderBy(x => x.ImageURL);
                var ImagesArray = comicImages.ToArray();

                for (int i = 0; i < ImagesArray.Count(); i++)
                {
                    ImagesArray[i] = _mapper.Map<ComicChapterImage>(updateChapterImageDTO[i]);

                    await _unitofwork.GetRepository<ComicChapterImage>().UpdateItemAsync(ImagesArray[i].Id, ImagesArray[i]);
                }

                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Update chapter image success");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
