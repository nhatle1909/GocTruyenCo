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

                Account query = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);
                if (query != null)
                {
                    var newRole = query.Role;
                    Enum.TryParse<Role>(role, out newRole);
                    query.Role = newRole;

                    await _unitofwork.GetRepository<Account>().UpdateItemAsync(id, query);
                    await _unitofwork.CommitAsync();
                    result.SuccessDeleteResponse();
                }
                return result;
            }
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAllAccountAsync()
        {

            ServiceResponse<IEnumerable<AccountDTO>> result = new ServiceResponse<IEnumerable<AccountDTO>>();

            var query = await _unitofwork.GetRepository<Account>().GetAllAsync();

            if (query != null)
            {
                var queryDTO = _mapper.Map<IEnumerable<AccountDTO>>(query);
                result.SuccessRetrieveResponse(queryDTO);
            }
            return result;
        }

        public async Task<ServiceResponse<IEnumerable<AccountDTO>>> GetPagingAsync(string[] fieldnames, string[] searchValue, string sortfield, bool isAsc, int pageSize, int skip)
        {
            ServiceResponse<IEnumerable<AccountDTO>> result = new ServiceResponse<IEnumerable<AccountDTO>>();

            var query = await _unitofwork.GetRepository<Account>().PagingAsync(fieldnames, searchValue, sortfield, isAsc, pageSize, skip);
            var queryDTO = _mapper.Map<IEnumerable<AccountDTO>>(query);

            result.SuccessRetrieveResponse(queryDTO);

            return result;
        }

        public async Task<ServiceResponse<AccountDTO>> GetByIdAsync(Guid id)
        {
            ServiceResponse<AccountDTO> result = new ServiceResponse<AccountDTO>();
            Account item = await _unitofwork.GetRepository<Account>().GetByIdAsync(id);
            if (item != null)
            {
                var itemDTO = _mapper.Map<AccountDTO>(item);
                result.SuccessRetrieveResponse(itemDTO);
            }
            return result;
        }

        public async Task<ServiceResponse<bool>> UpdateAccountAsync(UpdateAccountDTO updateAccountDTO)
        {
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            Account item = _mapper.Map<Account>(updateAccountDTO);

            bool query = await _unitofwork.GetRepository<Account>().UpdateItemAsync(item.Id, item);
            if (query)
            {
                await _unitofwork.CommitAsync();
                result.SuccessUpdateResponse();
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
                    result.SuccessDeleteResponse();
                }
                return result;
            }
        }
    }
}
