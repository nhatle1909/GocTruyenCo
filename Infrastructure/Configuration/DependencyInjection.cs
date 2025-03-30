
using Application.Interface;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {

            //ConnectionString
            services.AddSingleton<MongoDbOptions>(s =>
            {
                var uri = s.GetRequiredService<IConfiguration>()["ConnectionString"];
                return
                new MongoDbOptions
                {
                    ConnectionString = uri,
                    DatabaseName = s.GetRequiredService<IConfiguration>()["DatabaseName"]
                };
            });

            //Unitofwork
            services.AddScoped<IUnitofwork, Unitofwork>();
            //GenericRepository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //Cloudinary
            services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();
            services.AddScoped<ISendMailOTPRepository, SendMailOTPRepository>();

            services.AddSingleton(services => new Cloudinary(new Account(services.GetRequiredService<IConfiguration>()["Cloudinary:CloudName"],
                                                                         services.GetRequiredService<IConfiguration>()["Cloudinary:ApiKey"],
                                                                         services.GetRequiredService<IConfiguration>()["Cloudinary:ApiSecret"])));

        }
    }
}
