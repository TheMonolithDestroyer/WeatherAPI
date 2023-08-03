using AuthAPI.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Services
{
    public interface ITokenService
    {
        string CreateToken(IdentityUser user);
    }

    public class TokenService : ITokenService
    {
        private readonly ILogger<AccountManager> _logger;
        private const int ExpirationInMinutes = 30;

        public TokenService(ILogger<AccountManager> logger)
        {
            _logger = logger;
        }

        public string CreateToken(IdentityUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationInMinutes);
            var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
        {
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "apiWithAuthBackend",
                audience: "apiWithAuthBackend",
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return jwtSecurityToken;
        }

        private List<Claim> CreateClaims(IdentityUser user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, $"{user.UserName}-{user.Email}"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!)
                };
                return claims;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something happened while creating claims.", ex);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAru+OlQN/XBpYBDbP2XL7j6ztS2VJx/l7u/FPWgwx5v5gDung319+dh5ze/d9Mspkushi46f66uBUffJfwCb/KYNxS0lcAMezYHcZryy0uuBVUWg8vLiM/89gc2KME0FzjJHC32yTgb+Ddq5bCNFe636ELoPP5N6YiDN9hOo4r/Nz6kQGy66ioFv9kGRrNS1he9qBL2cVt0DjT1YacWQqReFa0R050Qv8vmdRmOWvrz9GT1Vh9oG1QrWD97IXvw9TQJGiVRwNEr8NLR9sN/+bEBCcRy1IWwhjxj6IMzXNppJhA0z2ceS5Bab8nLlOd/kLeBxmvUJa+DuybOkhdSwNPwIDAQAB")), SecurityAlgorithms.HmacSha256);
        }
    }
}
