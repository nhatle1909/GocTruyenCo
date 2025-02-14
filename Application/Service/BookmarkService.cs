using Application.Aggregation;
using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using static Domain.Enums.BookmarkEnum;
namespace Application.Service
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        public BookmarkService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> AddBookmark(CommandBookmarkDTO commandBookmarkDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var item = _mapper.Map<Bookmark>(commandBookmarkDTO);
                bool command = await _unitofwork.GetRepository<Bookmark>().AddOneItemAsync(item);
                if (!command)
                {
                    result.CustomResponse(false, false, "Add bookmark failed");
                    return result;
                }
                _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Add bookmark successfully");
            }
            catch (Exception ex)
            {
                result.CustomResponse(false, false, ex.Message);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteBookmark(Guid bookmarkId)
        {
            ServiceResponse<bool> result = new();
            try
            {
                bool command = await _unitofwork.GetRepository<Bookmark>().RemoveItemAsync(bookmarkId);
                if (!command)
                {
                    result.CustomResponse(false, false, "Delete bookmark failed");
                    return result;
                }
                _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Delete bookmark successfully");
            }
            catch (Exception ex)
            {
                result.CustomResponse(false, false, ex.Message);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryBookmarkDTO>>> GetPagingAsync(SearchDTO searchDTO)
        {
            ServiceResponse<IEnumerable<QueryBookmarkDTO>> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<Bookmark>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField,
                                                                              searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip, BookmarkAggregation.BookmarkBsonAggregation);
                var queryDTO = _mapper.Map<IEnumerable<QueryBookmarkDTO>>(query);
                result.CustomResponse(queryDTO, true, "Retrieve data successful");
            }
            catch (Exception ex)
            {
                result.CustomResponse(null, false, ex.Message);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateBookmarkType(Guid bookmarkId, string bookmarkType)
        {
            ServiceResponse<bool> result = new();
            try
            {
                var item = await _unitofwork.GetRepository<Bookmark>().GetByIdAsync(bookmarkId);
                var newType = item.BookmarkType;
                Enum.TryParse<BookmarkType>(bookmarkType, out newType);
                item.BookmarkType = newType;

                bool command = await _unitofwork.GetRepository<Bookmark>().UpdateItemAsync(bookmarkId, item);
                if (!command)
                {
                    result.CustomResponse(false, false, "Update bookmark failed");
                    return result;
                }
                _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Update bookmark successfully");
            }
            catch (Exception ex)
            {
                result.CustomResponse(false, false, ex.Message);
            }
            return result;
        }
    }
}
