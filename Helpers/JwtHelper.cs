using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TestApi.Helpers
{
    public static class JwtHelper
    {
        private const string _key = "secret_key_key_key_key_123_key_123_key_123_key_123";
        private static readonly SymmetricSecurityKey _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

        public static string GenerateJwtToken(string userId, string userName, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                //Expires = DateTime.UtcNow.AddMinutes(5), // Время жизни токена
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256),
                Issuer = "MyTestToken",
                Audience = "TestToken"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);

            return tokenHandler.WriteToken(token);
        }
    }
}
