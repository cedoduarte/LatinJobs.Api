using LatinJobs.Api.Services.Interfaces;
using LatinJobs.Api.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LatinJobs.Api.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public string GetJwtToken(int userId, int expirationSeconds)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(Constants.Jwt.UserIdClaim, $"{userId}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"]!,
                _configuration["Jwt:Audience"]!,
                claims,
                expires: DateTime.UtcNow.AddSeconds(expirationSeconds),
                signingCredentials: signIn);

            string jwtGeneratedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtGeneratedToken;
        }
    }
}
