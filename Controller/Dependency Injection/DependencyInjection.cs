using Application.Configuration;
using Application.DTO;
using Controller.FluentValidation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            //Authenticate
            services.AddTransient<IValidator<AuthenticateDTO>, AuthenticateDTOValidation>();
            services.AddTransient<IValidator<SignUpDTO>, SignUpDTOValidation>();
            //Account
            services.AddTransient<IValidator<CommandAccountDTO>, CommandAccountDTOValidation>();
        }
        public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            //Environment variable
            var validIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            var validAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET");

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                  
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? throw new ArgumentNullException("builder.Configuration[\"Jwt:secretkey\"]", "Jwt:secretkey is null"))),
                    ValidIssuer = validIssuer ?? configuration.GetRequiredSection("JWT:issuer").Value,
                    ValidAudience = validAudience ?? configuration.GetRequiredSection("JWT:audience").Value,
                    ClockSkew = TimeSpan.Zero
                };

            });
            services.AddAuthorization();

            //Controller
            services.AddControllers();
        }
    }
}
