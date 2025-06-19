using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoServices.Models;

namespace TodoServices.Services.Token
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string CreateToken(UserModel user)
        {
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()), new Claim(ClaimTypes.Name, user.Username), new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: config["Jwt:Issuer"], audience: config["Jwt:Audience"], claims: claims, expires: DateTime.Now.AddHours(1), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
