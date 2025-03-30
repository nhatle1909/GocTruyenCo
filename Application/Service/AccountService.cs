using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;

namespace Application.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;

        public AccountService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> UpdateRoleAsync(Guid id, string role)
        {
         
            {
                ServiceResponse<bool> result = new();
                try
                {
                    Account query = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);

                    if (query == null)
                    {
                        result.CustomResponse(false, false, "Account not found");
                        return result;
                    }

                    var newRole = query.Role;
                    Enum.TryParse<Role>(role, out newRole);
                    query.Role = newRole;

                    await _unitofwork.GetRepository<Account>().UpdateItemAsync(id, query);
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Role updated successful");
                }
                catch (Exception ex)
                {
                    result.TryCatchResponse(ex);
                }
                return result;
            }
        }

        public async Task<ServiceResponse<IEnumerable<QueryAccountDTO>>> GetAllAccountAsync()
        {

            ServiceResponse<IEnumerable<QueryAccountDTO>> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<Account>().GetAllAsync();

                var queryDTO = _mapper.Map<IEnumerable<QueryAccountDTO>>(query);
                result.CustomResponse(queryDTO, true, "Retrieve data successful");

            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryAccountDTO>>> GetPagingAsync(SearchDTO searchDTO)
        {

            ServiceResponse<IEnumerable<QueryAccountDTO>> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<Account>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField
                    , searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip);
                var queryDTO = _mapper.Map<IEnumerable<QueryAccountDTO>>(query);
                if (queryDTO == null)
                {
                    result.CustomResponse(queryDTO, false, "No data retrieved ");
                    return result;
                }
                result.CustomResponse(queryDTO, true, "Retrieve data successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<QueryAccountDTO>> GetByIdAsync(Guid id)
        {
            ServiceResponse<QueryAccountDTO> result = new();
            try
            {
                Account item = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);
                if (item == null)
                {
                    result.CustomResponse(null, false, "Account not found");
                    return result;
                }

                var itemDTO = _mapper.Map<QueryAccountDTO>(item);
                result.CustomResponse(itemDTO, true, "Retrieve data successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateAccountAsync(Guid accountId, CommandAccountDTO commandAccountDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                Account item = _mapper.Map<Account>(commandAccountDTO);
                item.Id = accountId;
                bool query = await _unitofwork.GetRepository<Account>().UpdateItemAsync(accountId, item);
                if (!query)
                {
                    result.CustomResponse(false, false, "Data updated fail");
                    return result;
                }

                await _unitofwork.CommitAsync();
                result.CustomResponse(true, true, "Data updated successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> RestrictAccountAsync(Guid id)
        {
            {
                ServiceResponse<bool> result = new();

                Account query = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);
                if (query == null)
                {
                    result.CustomResponse(false, false, "Account not found");
                    return result;
                }

                if (query.isRestricted == true)
                {
                    query.isRestricted = false;
                    result.CustomResponse(true, true, "Unrestrict account successful");

                }
                else
                {
                    query.isRestricted = true;
                    result.CustomResponse(true, true, "Restrict account successful");
                }
                await _unitofwork.GetRepository<Account>().UpdateItemAsync(id, query);
                await _unitofwork.CommitAsync();


                return result;
            }
        }

        public async Task<ServiceResponse<long>> CountAsync(CountDTO countDTO)
        {
            ServiceResponse<long> result = new();
            try
            {
                var query = await _unitofwork.GetRepository<Account>().CountAsync(countDTO.searchFields, countDTO.searchValues, countDTO.pageSize);
                result.CustomResponse(query, true, "Count successful");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
