using GifsAppv2.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GifsAppv2.Services.Token
{
    public class Token
    {
        // Create a new token
        public static string CreateToken(IConfiguration configuration, User user)
        {
            // Create token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = configuration!["GifsApp:JwtSecret"];
            var byteKey = Encoding.UTF8.GetBytes(jwtKey!);

            // Content description
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("idUser", user.IdUser.ToString()),
                    new Claim("username", user.Username!),
                    new Claim("email", user.Email!)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        // Validate if token is valid
        public static async Task<bool> ValidateToken(IConfiguration configuration, string? token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return false;
                }

                // Decode token
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

                string jwtKey = configuration["GifsApp:JwtSecret"]!;
                var key = Encoding.UTF8.GetBytes(jwtKey);

                // Create validation options
                var validationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };

                // Validate token
                var jwtToken = await tokenHandler.ValidateTokenAsync(token, validationParameters);

                if (!jwtToken.IsValid)
                {
                    return false;
                }

                // Get claims from token
                var expiration = jwtSecurityToken.Claims.First(c => c.Type == "exp").Value;
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiration)).UtcDateTime;

                if (expirationDateTime < DateTimeOffset.UtcNow)
                {
                    // Token expired
                    return false;
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
