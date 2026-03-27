using DSS2_Backend.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
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

            string issuer = jsonWebToken["Issuer"]!.ToString();
            string audience = jsonWebToken["Audience"]!.ToString();
            string key = jsonWebToken["key"]!.ToString();
            int expiresMinutes = int.Parse(jsonWebToken["ExpiresMinutes"] ?? "60");

            var claims = new Dictionary<string, object>
            {
                ["sub"] = user.Id.ToString(),
                ["email"] = user.Email,
                ["role"] = user.Roles.ToString()
            };

            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            SigningCredentials credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Claims = claims,
                Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
                SigningCredentials = credentials
            };

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);

        }
    }
}
