using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;

namespace Application.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        private readonly string[] emailField = ["Email"];
        public AuthenticateService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> SignInAsync(AuthenticateDTO authenticateDTO)
        {
            ServiceResponse<string> result = new();

         
            string[] loginData = [authenticateDTO.Email];

            var query = await _unitofwork.GetRepository<Account>().GetAllByFilterAsync(emailField, loginData);
            var account = query.FirstOrDefault();

            if (account == null)
            {
                result.CustomResponse("", false, "Account not found");
                return result;
            }
            
            if (account.Password != authenticateDTO.Password)
            {
                result.CustomResponse("", false, "Password is incorrect");
                return result;
            }

            var token = JWT.GenerateJwtToken(account.Id.ToString(), account.Username, account.Email, account.Role.ToString());
            result.CustomResponse(token, true, "SignIn successful");
            return result;
        }

        public async Task<ServiceResponse<bool>> SignOutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<string>> ResetPassword(string email)
        {
            throw new NotImplementedException();
        }
        public async Task<ServiceResponse<bool>> SignUpAsync(SignUpDTO signupDTO)
        {
            ServiceResponse<bool> result = new();
            try
            {
                Account item = _mapper.Map<Account>(signupDTO);
      
                string[] emailValue = [signupDTO.Email];

                var query = await _unitofwork.GetRepository<Account>().GetAllByFilterAsync(emailField, emailValue);

                if (query.Count() > 0)
                {
                    result.CustomResponse(false, false, "Email already exist");
                    return result;
                }

                bool command = await _unitofwork.GetRepository<Account>().AddOneItemAsync(item);

                if (command)
                {
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Register successful");
                }
            }
            catch (Exception ex)
            {
                result.TryCatchResponse(ex);
            }
            return result;
        }
    }
}
