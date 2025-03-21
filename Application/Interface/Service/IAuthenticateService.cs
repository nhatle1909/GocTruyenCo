using Application.Common;
using Application.DTO;

namespace Application.Interface.Service
{
    public interface IAuthenticateService
    {
        Task<ServiceResponse<string>> SignInAsync(AuthenticateDTO authenticateDTO);
        Task<ServiceResponse<bool>> SendOTPMail(string email);
        Task<ServiceResponse<bool>> VerifyOTP(string email,string OTP);
        Task<ServiceResponse<bool>> SignOutAsync();
        Task<ServiceResponse<string>> ResetPassword(string email);
        Task<ServiceResponse<bool>> ChangePassword(AuthenticateDTO authenticateDTO);
        Task<ServiceResponse<bool>> SignUpAsync(SignUpDTO signUpDTO);
    }
}
