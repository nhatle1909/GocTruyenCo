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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie()
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(.GetConfiguration["JWT:Key"] ?? throw new ArgumentNullException("builder.Configuration[\"Jwt:Key\"]", "Jwt:Key is null"))),
                    ValidIssuer = .Configuration["JWT:Issure"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ClockSkew = TimeSpan.Zero
                };
            }); ;
            services.AddAuthorization();

            //Controller
            services.AddControllers();
        }
    }
}
