using Application.Common;
using Application.DTO;
using Application.Interface;
using Application.Interface.Service;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IMapper _mapper;
        private readonly string[] emailField = ["Email"];
        private readonly IConfiguration _configuration;
        private readonly JWT _jwt;
        public AuthenticateService(IUnitofwork unitofwork, IMapper mapper, IConfiguration configuration)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _configuration = configuration;
            _jwt = new JWT(_configuration);
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
            if (account.isRestricted)
            {
                result.CustomResponse("", false, "Your account is restricted!");
                return result;
            }
            var token = _jwt.GenerateJwtToken(account.Id.ToString(), account.Role.ToString(), account.Username, account.Email);
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

        public async Task<ServiceResponse<bool>> ChangePassword(AuthenticateDTO authenticateDTO)
        {

            ServiceResponse<bool> result = new();
            try
            {
                Account item = _mapper.Map<Account>(authenticateDTO);

                string[] emailValue = [authenticateDTO.Email];

                var query = await _unitofwork.GetRepository<Account>().GetAllByFilterAsync(emailField, emailValue);

                if (query.Count() == 0)
                {
                    result.CustomResponse(false, false, "Email does not exist");
                    return result;
                }

                var account = query.FirstOrDefault();
                bool command = await _unitofwork.GetRepository<Account>().UpdateItemAsync(account.Id, item); ;

                if (command)
                {
                    await _unitofwork.CommitAsync();
                    result.CustomResponse(true, true, "Change password successful");
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
