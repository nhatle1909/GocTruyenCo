using Application.Common;
using Application.DTO;
using Domain.Entities;
using Domain.Enums;

namespace Application.Interface.Service
{
    public interface IAccountService
    {
        Task<ServiceResponse<bool>> UpdateRoleAsync(Guid accountId,string role);
        Task<ServiceResponse<bool>> RestrictAccountAsync(Guid id);
        Task<ServiceResponse<bool>> UpdateAccountAsync(UpdateAccountDTO updateAccountDTO);
        Task<ServiceResponse<AccountDTO>> GetByIdAsync(Guid id);
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetAllAccountAsync();
        Task<ServiceResponse<IEnumerable<AccountDTO>>> GetPagingAsync(string[] fieldnames, string[] searchValue, string sortfield, bool isAsc, int pageSize, int skip);
    }
}
