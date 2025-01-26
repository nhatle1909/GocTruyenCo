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
        Task<ServiceResponse<bool>> UpdateAccountAsync(Guid accountId,CommandAccountDTO updateAccountDTO);
        Task<ServiceResponse<QueryAccountDTO>> GetByIdAsync(Guid id);
        Task<ServiceResponse<IEnumerable<QueryAccountDTO>>> GetAllAccountAsync();
        Task<ServiceResponse<IEnumerable<QueryAccountDTO>>> GetPagingAsync(SearchDTO searchDTO);
    }
}
