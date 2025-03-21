using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ISendMailOTPRepository
    {
        Task<bool> SendEmailAsync(string email, string otp);
        Task<string> OTPGenerator(string email);
        Task<bool> VerifyOTP(string email, string otp);
    }
}
