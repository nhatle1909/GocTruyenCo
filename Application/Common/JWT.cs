using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Common
{
    public class JWT
    {
        private IConfiguration _configuration;
        public static string secretKey = "";
        public static string issuer = "";
        public static string audience = "";
        public static double expiryMinutes = 0;
        public JWT(IConfiguration configuration)
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT:secretkey");
                var issuer = Environment.GetEnvironmentVariable("JWT:issuer");
                var audience = Environment.GetEnvironmentVariable("JWT:audience");
                var expiryMinutesFromEnv = Environment.GetEnvironmentVariable("JWT:expire");

                if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(expiryMinutesFromEnv))
                {
                    throw new Exception("JWT configuration is missing in environment variables.");
            }
            _configuration = configuration;
            secretKey = secretKey ?? _configuration.GetSection("JWT:secretkey").Value;
            issuer = issuer ?? _configuration.GetSection("JWT:issuer").Value;
            audience = audience ?? _configuration.GetSection("JWT:audience").Value;
            expiryMinutes = Double.TryParse(expiryMinutesFromEnv, out var result) ? result : Double.Parse(_configuration.GetSection("JWT:expire").Value);
        }

        public string GenerateJwtToken(string userId, string role, string username, string email)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role,role),
                new Claim(ClaimTypes.Name,username),
                new Claim(ClaimTypes.Email,email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));

            // 3. Create Signing Credentials
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Create Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes), // Token expiration time
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = creds,
            };



            // 5. Generate Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 6. Return Token as String
            return tokenHandler.WriteToken(token);
        }
    }
}
