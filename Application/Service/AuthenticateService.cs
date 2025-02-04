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
        public AuthenticateService(IUnitofwork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<string>> SignInAsync(AuthenticateDTO authenticateDTO)
        {
            ServiceResponse<string> result = new ServiceResponse<string>();

            string[] loginFields = ["Email", "Password"];
            string[] loginData = [authenticateDTO.Email, authenticateDTO.Password];

            var query = await _unitofwork.GetRepository<Account>().GetAllByFilterAsync(loginFields, loginData);
            var account = query.FirstOrDefault();

            if (account != null)
            {
                var token = JWT.GenerateJwtToken(account.Id.ToString(), account.Username, account.Email, account.Role.ToString());
                result.SuccessRetrieveResponse(token);
            }
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
            ServiceResponse<bool> result = new ServiceResponse<bool>();
            try
            {
                Account item = _mapper.Map<Account>(signupDTO);
                string[] email = ["Email"];
                string[] emailValue = [signupDTO.Email];
                var query = await _unitofwork.GetRepository<Account>().GetAllByFilterAsync(email, emailValue);

                if (query.Count() > 0)
                {
                    result.CustomResponse(false, false, "Email already exist");
                    return result;
                }

                bool command = await _unitofwork.GetRepository<Account>().AddOneItemAsync(item);

                if (command)
                {
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Register succesfully");
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
