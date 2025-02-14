using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class ComicChapterService : IComicChapterService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public ComicChapterService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> CreateChapter(CommandComicChapterDTO commandComicChapterDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var comicChapter = _mapper.Map<ComicChapter>(commandComicChapterDTO);
                bool command = await _unitofwork.GetRepository<ComicChapter>().AddOneItemAsync(comicChapter);
                if (!command)
                {
                    result.CustomResponse(false, false, "Create chapter failed");
                    return result;
                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Create chapter successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryComicChapterDTO>>> GetAllChapter(Guid comicId)
        {
            ServiceResponse<IEnumerable<QueryComicChapterDTO>> result = new();
            try
            {
                string[] searchField = ["ComicId"];
                string[] searchValue = [comicId.ToString()];
                var query = await _unitofwork.GetRepository<ComicChapter>().GetAllByFilterAsync(searchField, searchValue);

                var data = _mapper.Map<IEnumerable<QueryComicChapterDTO>>(query);
                result.CustomResponse(data, true, "Get all chapter successful");

            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryComicChapterDTO>>> GetChaptersPaging(SearchDTO searchDTO)
        {
            ServiceResponse<IEnumerable<QueryComicChapterDTO>> result = new();
            try
            {

                var query = await _unitofwork.GetRepository<ComicChapter>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField,
                                                                                        searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip);

                var data = _mapper.Map<IEnumerable<QueryComicChapterDTO>>(query);
                result.CustomResponse(data, true, "Get all chapter successful");

            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateChapter(Guid chapterId, CommandComicChapterDTO commandComicChapterDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<ComicChapter>().GetByIdAsync(chapterId);
                if (query == null)
                {
                    result.CustomResponse(false, false, "Chapter not found");
                }

                var comicChapter = _mapper.Map<ComicChapter>(commandComicChapterDTO);
                comicChapter.Id = chapterId;
                bool command = await _unitofwork.GetRepository<ComicChapter>().UpdateItemAsync(chapterId, comicChapter);

                if (!command)
                {
                    result.CustomResponse(false, false, "Update chapter failed");
                    return result;

                }
                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Update chapter successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
