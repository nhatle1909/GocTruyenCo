
using Application.Interface;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        
       
        public static void AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            var databaseName = configuration["DatabaseName"];
            var cloudinaryName = configuration["Cloudinary:CloudName"];
            var cloudinaryApiKey = configuration["Cloudinary:ApiKey"];
            var cloudinaryApiSecret = configuration["Cloudinary:ApiSecret"];
            services.Configure<MongoDbOptions>(options =>
            {
                options.ConnectionString = connectionString ?? throw new Exception("ConnectionString is missing from Environment/Config");
                options.DatabaseName = databaseName ?? throw new Exception("DatabaseName is missing from Environment/Config");
            });


            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoDbOptions>>().Value);

            services.AddSingleton(sp =>
            {
                var cloudName = cloudinaryName;
                var apiKey = cloudinaryApiKey;
                var apiSecret = cloudinaryApiSecret;

                if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("Cloudinary settings are missing from Environment/Config");
                }

                return new Cloudinary(new Account(cloudName, apiKey, apiSecret));
            });

            // 3. Repositories
            services.AddScoped<IUnitofwork, Unitofwork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();
            services.AddScoped<ISendMailOTPRepository, SendMailOTPRepository>();
        }
    }
}
