using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Common
{
    public class JWT
    {
        //private IConfiguration _configuration;
        //public static string secretKey = "";
        //public static string issuer = "";
        //public static string audience = "";
        //public static double expiryMinutes = 0;
        //public JWT(IConfiguration configuration)
        //{
        //    var secretKey = Environment.GetEnvironmentVariable("JWT:secretkey");
        //    var issuer = Environment.GetEnvironmentVariable("JWT:issuer");
        //    var audience = Environment.GetEnvironmentVariable("JWT:audience");
        //    var expiryMinutesFromEnv = Environment.GetEnvironmentVariable("JWT:expire");

        //    if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(expiryMinutesFromEnv))
        //    {
        //            throw new Exception("JWT configuration is missing in environment variables.");
        //    }
        //    _configuration = configuration;
        //    secretKey = secretKey ?? _configuration.GetSection("JWT:secretkey").Value;
        //    issuer = issuer ?? _configuration.GetSection("JWT:issuer").Value;
        //    audience = audience ?? _configuration.GetSection("JWT:audience").Value;
        //    expiryMinutes = Double.TryParse(expiryMinutesFromEnv, out var result) ? result : Double.Parse(_configuration.GetSection("JWT:expire").Value);
        //}
        private readonly JWTSettings _settings;

        // Constructor receives the settings automatically via Dependency Injection
        public JWT(IOptions<JWTSettings> options)
        {
            _settings = options.Value;

            // Simple validation to ensure settings were loaded
            if (string.IsNullOrEmpty(_settings.SecretKey))
                throw new Exception("JWT Secret Key is missing from configuration.");
        }
        public string GenerateJwtToken(string userId, string role, string username, string email)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role,role),
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Email,email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_settings.SecretKey));

            // 3. Create Signing Credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Create Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes), // Token expiration time
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = creds,
            };



            // 5. Generate Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 6. Return Token as String
            return tokenHandler.WriteToken(token);
        }
       
    }
    public class JWTSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public double ExpiryMinutes { get; set; }
    }
}
