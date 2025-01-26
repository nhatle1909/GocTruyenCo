using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace Application.Common
{
    public class JWT
    {
        private readonly IConfiguration _configuration;
        private static string secretKey = "";
        private static string issuer = "";
        private static string audience = "";
        private static double expiryMinutes = 5;
        public JWT(IConfiguration configuration)
        {
            _configuration = configuration;
            secretKey = _configuration.GetSection("JWT")["SecretKey"];
            issuer = _configuration.GetSection("JWT")["issuer"];
            audience = _configuration.GetSection("JWT")["audience"];
            expiryMinutes = Double.Parse(_configuration.GetSection("JWT")["expire"]);
        }

        public static string GenerateJwtToken(string userId, string role, string username, string email)
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
