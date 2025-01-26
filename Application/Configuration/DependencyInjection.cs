using Application.Interface.Service;
using Application.Service;


using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class DependencyInjection
    {
        public static void AddApplicationService(this IServiceCollection services)
        {

            //Authenticate
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            //Account
            services.AddTransient<IAccountService, AccountService>();
            //AutoMapper
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}
