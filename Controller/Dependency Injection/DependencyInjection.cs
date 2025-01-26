using Application.Configuration;
using Application.DTO;
using Controller.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;

namespace Controller.Dependency_Injection
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddInfrastructureService();
        }
        public static void AddService(this IServiceCollection services)
        {
            services.AddApplicationService();
        }
        public static void AddValidation(this IServiceCollection services)
        { 
            services.AddTransient<IValidator<CommandAccountDTO>, CommandAccountDTOValidation>();
        }
        public static void AddSettings(this IServiceCollection services)
        {
            //CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            //MemoryCache
            services.AddMemoryCache();

            //FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            //Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
            });
            //Authentication & Authorization
            services.AddAuthentication();
            services.AddAuthorization();

            //Controller
            services.AddControllers();
        }
    }
}
