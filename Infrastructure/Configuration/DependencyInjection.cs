
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
            //Environment Variable
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            var databaseName = Environment.GetEnvironmentVariable("DatabaseName");
            var cloudinaryName = Environment.GetEnvironmentVariable("Cloudinary:CloudName");
            var cloudinaryApiKey = Environment.GetEnvironmentVariable("Cloudinary:ApiKey");
            var cloudinaryApiSecret = Environment.GetEnvironmentVariable("Cloudinary:ApiSecret");

            //ConnectionString
            services.AddSingleton<MongoDbOptions>(s =>
            {
                var uri = connectionString ?? s.GetRequiredService<IConfiguration>()["ConnectionString"];
                return
                new MongoDbOptions
                {
                    ConnectionString = uri,
                    DatabaseName =databaseName ?? s.GetRequiredService<IConfiguration>()["DatabaseName"]
                };
            });

            //Unitofwork
            services.AddScoped<IUnitofwork, Unitofwork>();
            //GenericRepository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //Cloudinary
            services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();
            services.AddScoped<ISendMailOTPRepository, SendMailOTPRepository>();

            services.AddSingleton(services => new Cloudinary(new Account(cloudinaryName ?? services.GetRequiredService<IConfiguration>()["Cloudinary:CloudName"],
                                                                         cloudinaryApiKey ?? services.GetRequiredService<IConfiguration>()["Cloudinary:ApiKey"],
                                                                         cloudinaryApiSecret ?? services.GetRequiredService<IConfiguration>()["Cloudinary:ApiSecret"])));

        }
    }
}
