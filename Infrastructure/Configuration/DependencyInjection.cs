
using Application.Interface;


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

        }
    }
}
