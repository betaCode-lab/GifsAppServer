using GifsAppv2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GifsAppv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Attributes
        private readonly GifsContext _context;
        private readonly IConfiguration _configuration;

        // Controller
        public AuthController(GifsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Action

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<string>> Login(Login credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == credentials.Email);

            if (user == null)
            {
                return BadRequest("This user not exists.");
            }

            if (!BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password))
            {
                return BadRequest("Password incorrect.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];
            var byteKey = Encoding.UTF8.GetBytes(jwtKey!);

            // We created a token content description 
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("IdUser", user.IdUser.ToString()),
                    new Claim("Username", user.Username!),
                    new Claim("Email", user.Email!)
                }),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
