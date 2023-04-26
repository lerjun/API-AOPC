using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthSystem.Manager;
using AuthSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace AuthSystem.Services
{
    public class ApiGlobalModel
    {
        public string Token { get; set; }
        public int PageStatus { get; set; }
        public string Status { get; set; }
        public string PageLink { get; set; }
        public string UserId { get; set; }
        public string GenerateToken(string userName, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Version,"V3.5")
                }),
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var userToken = tokenHandler.WriteToken(token);
            var result = Cryptography.Encrypt(userToken);
            return userToken;
        }
    }
}
