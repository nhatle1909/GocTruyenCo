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
            //ComicCategory
            services.AddTransient<IComicCategoryService, ComicCategoryService>();
            //Comic
            services.AddTransient<IComicService, ComicService>();
            //AutoMapper
            services.AddAutoMapper(typeof(MapperProfile));
            //Cloudinary
            services.AddScoped<ICloudinaryService, CloudinaryService>();
        }
    }
}
