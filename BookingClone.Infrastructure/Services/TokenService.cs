using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Services
{
    public class TokenService(IConfiguration config) : ITokenService
    {
        public string GenerateToken(User user, AppRole appRole)
        {
            var tokenKey = config["JwtConfig:TokenKey"] ?? throw new Exception("Unable to get token key from appsettings.json");
            var expiration = config["JwtConfig:LifeTime"] ?? throw new Exception("Unable to get token lifetime from appsettings.json");

            if (!int.TryParse(expiration, out int expirationInSeconds)) //parse expiration in int 
            {
                throw new Exception("Invalid token lifetime value in appsettings.json");
            }


            if (tokenKey.Length < 64) throw new InvalidOperationException("Token key must be at least 64 characters long"); //for security reasons

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

            if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException("No email provided for user!");

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, appRole.ToString())
                };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddSeconds(expirationInSeconds),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
