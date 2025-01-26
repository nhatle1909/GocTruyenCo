using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        private readonly ILogger<AccountController> _logger;

        public AuthenticateController(IAuthenticateService authenticateService, ILogger<AccountController> logger)
        {
            _authenticateService = authenticateService;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateDTO authenticateDTO)
        {
            var query = await _authenticateService.SignInAsync(authenticateDTO);
            return Ok(query);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] SignUpDTO signUpDTO)
        {
            var query = await _authenticateService.SignUpAsync(signUpDTO);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
    }
}
