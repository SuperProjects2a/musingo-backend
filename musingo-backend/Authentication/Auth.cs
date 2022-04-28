using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using musingo_backend.Models;

namespace musingo_backend.Authentication
{
    public class Auth :IJwtAuth
    {
        private readonly string _key;
        public Auth(string key)
        {
            _key = key;
        }
        public string Authentication(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("id", user.Id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var roles = Enum.GetValues(user.Role.GetType()).Cast<Enum>().Where(user.Role.HasFlag);
            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim("role",role.ToString()));
            }
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
