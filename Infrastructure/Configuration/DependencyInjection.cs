
using Application.Interface;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {

            //ConnectionString
            services.AddSingleton<IMongoClient, MongoClient>(s =>
            {
                var uri = s.GetRequiredService<IConfiguration>()["ConnectionString"];
                return new MongoClient(uri);
            });

            //Unitofwork
            services.AddScoped<IUnitofwork, Unitofwork>();
            //GenericRepository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //Cloudinary
            services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();

            services.AddSingleton(services => new Cloudinary(new Account(services.GetRequiredService<IConfiguration>()["Cloudinary:CloudName"],
                                                                         services.GetRequiredService<IConfiguration>()["Cloudinary:ApiKey"],
                                                                         services.GetRequiredService<IConfiguration>()["Cloudinary:ApiSecret"])));

        }
    }
}
