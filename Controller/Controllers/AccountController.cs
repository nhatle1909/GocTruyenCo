using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> PagingAsync([FromQuery] SearchDTO searchDTO)
        {
            var query = await _accountService.GetPagingAsync(searchDTO);
            return Ok(query);
        }
        [HttpGet("{accountId}")]
        public async Task<IActionResult> Get(Guid accountId)
        {
            var query = await _accountService.GetByIdAsync(accountId);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("/Role/{accountId}")]
        public async Task<IActionResult> UpdateRole(Guid accountId, string role)
        {
            var query = await _accountService.UpdateRoleAsync(accountId, role);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpPut("{accountId}")]
        public async Task<IActionResult> Put(Guid accountId, CommandAccountDTO updateAccountDTO)
        {

            var query = await _accountService.UpdateAccountAsync(accountId, updateAccountDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [Authorize(Roles= "Admin")]
        [HttpDelete("{accountid}")]
        public async Task<IActionResult> Restrict(Guid accountId)
        {
            var query = await _accountService.RestrictAccountAsync(accountId);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
    }
}
