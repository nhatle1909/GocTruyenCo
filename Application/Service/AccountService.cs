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
                ServiceResponse<bool> result = new ServiceResponse<bool>();
                try
                {
                    Account query = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);
                    if (query != null)
                    {
                        var newRole = query.Role;
                        Enum.TryParse<Role>(role, out newRole);
                        query.Role = newRole;

                        await _unitofwork.GetRepository<Account>().UpdateItemAsync(id, query);
                        await _unitofwork.CommitAsync();
                        result.CustomResponse(true, true, "Role Updated Successfully");
                    }
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

            ServiceResponse<IEnumerable<QueryAccountDTO>> result = new ServiceResponse<IEnumerable<QueryAccountDTO>>();
            try
            {
                var query = await _unitofwork.GetRepository<Account>().GetAllAsync();

                if (query != null)
                {
                    var queryDTO = _mapper.Map<IEnumerable<QueryAccountDTO>>(query);
                    result.CustomResponse(queryDTO, true, "Success Retrieve Data");
                }
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<QueryAccountDTO>>> GetPagingAsync(SearchDTO searchDTO)
        {

            ServiceResponse<IEnumerable<QueryAccountDTO>> result = new ServiceResponse<IEnumerable<QueryAccountDTO>>();
            try
            {
                var query = await _unitofwork.GetRepository<Account>().PagingAsync(searchDTO.searchFields, searchDTO.searchValues, searchDTO.sortField
                    , searchDTO.sortAscending, searchDTO.pageSize, searchDTO.skip);
                var queryDTO = _mapper.Map<IEnumerable<QueryAccountDTO>>(query);

                result.CustomResponse(queryDTO, true, "Success Retrieve Data");
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<QueryAccountDTO>> GetByIdAsync(Guid id)
        {
            ServiceResponse<QueryAccountDTO> result = new ServiceResponse<QueryAccountDTO>();
            try
            {
                Account item = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);
                if (item != null)
                {
                    var itemDTO = _mapper.Map<QueryAccountDTO>(item);
                    result.CustomResponse(itemDTO, true, "Success Retrieve Data");
                }
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateAccountAsync(Guid accountId, CommandAccountDTO commandAccountDTO)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            try
            {
                Account item = _mapper.Map<Account>(commandAccountDTO);

                bool query = await _unitofwork.GetRepository<Account>().UpdateItemAsync(accountId, item);
                if (query)
                {
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Data Updated Successfully");
                }
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
                ServiceResponse<bool> result = new ServiceResponse<bool>();

                Account query = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);
                if (query != null)
                {
                    query.isRestricted = true;
                    await _unitofwork.GetRepository<Account>().UpdateItemAsync(id, query);
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Restrict Account Successfully");
                }
                return result;
            }
        }
    }
}
