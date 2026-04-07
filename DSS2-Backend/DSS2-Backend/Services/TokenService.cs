using DSS2_Backend.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DSS2_Backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string CreateAccessToken(User user)
        {
            IConfigurationSection jsonWebToken = _configuration.GetSection("Jwt");

            string issuer = jsonWebToken["Issuer"]!;
            string audience = jsonWebToken["Audience"]!;
            string key = jsonWebToken["key"]!;
            int expiresMinutes = int.Parse(jsonWebToken["ExpiresMinutes"] ?? "60");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            SigningCredentials credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
                SigningCredentials = credentials
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
