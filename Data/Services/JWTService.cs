using FixIt_Backend.Data.Models.Custom;
using FixIt_Backend.Data.Models.Database;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FixIt_Backend.Data.Services
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       

        public string GenerateJWTToken(User user, string userName)
        {
            var jwt = _configuration.GetSection("JWT").Get<JWTModel>();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.Role,user.Role.Name.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwt.ExpiresMinute))
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
