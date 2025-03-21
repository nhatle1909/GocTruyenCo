using Application.DTO;
using Application.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Controller.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AuthenticateController(IAuthenticateService authenticateService, ILogger<AccountController> logger,IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
            _logger = logger;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateDTO authenticateDTO)
        {
            var query = await _authenticateService.SignInAsync(authenticateDTO);

            if (!query.Success)
            {
                return BadRequest(query);
            }
          
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
        [HttpPost("SendOTPMail")]
        public async Task<IActionResult> SendOTPMail( string email)
        {
            var query = await _authenticateService.SendOTPMail(email);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP( string email, string OTP)
        {
            var query = await _authenticateService.VerifyOTP(email,OTP);
            if (!query.Success)
            {
                return BadRequest(query);
            }
            return Ok(query);
        }
    }
}
