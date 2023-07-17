using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UATP.RapidPay.Shared.Jwt
{
    public class JwtManager : IJwtManager
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public JwtManager(JwtConfiguration jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }

        public (string, DateTime) GenerateToken(int userId)
        {
            string issuer = _jwtConfiguration.Issuer;
            string audience = _jwtConfiguration.Audience;
            string secretKey = _jwtConfiguration.SecretKey;
            var exp = DateTime.UtcNow.AddMinutes(_jwtConfiguration.ExpireMinutes);

            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var _signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var _header = new JwtHeader(_signingCredentials);

            var _claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserID", userId.ToString())
            };

            var _Payload = new JwtPayload(
                    issuer: issuer,
                    audience: audience,
                    claims: _claims,
                    notBefore: DateTime.UtcNow,
                    expires: exp
                );

            var _Token = new JwtSecurityToken(_header, _Payload);

            return (new JwtSecurityTokenHandler().WriteToken(_Token), exp);
        }
    }
}
