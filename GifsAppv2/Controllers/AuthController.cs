using GifsAppv2.Models;
using GifsAppv2.Services.Email;
using GifsAppv2.Services.Token;
using Microsoft.AspNetCore.Authorization;
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
            try
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

                string token = Token.CreateToken(_configuration, user);

                return Ok(new { Token = token });
            }
            catch
            {
                throw new Exception("An error occurred, please try again later.");
            }
        }

        [HttpGet]
        [Route("AuthenticateAccount/{token}")]
        public async Task<ActionResult> AuthenticateAccount(string token)
        {
            try
            {
                return Ok();
            }
            catch
            {
                throw new Exception("An error occurred while authenticating the account.");
            }
        }

        [HttpPost]
        [Route("SendPasswordResetEmail")]
        public async Task<ActionResult> SendPasswordResetEmail(ChangePassword changePassword)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(changePassword.Email))
                {
                    return BadRequest("Email is required.");
                }

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == changePassword.Email);

                if (user == null)
                {
                    return BadRequest("This user not exists.");
                }

                string token = Token.CreateToken(_configuration, user);

                user.Token = token;
                await _context.SaveChangesAsync();

                EmailService.ResetPassword(_configuration, user.Email!, token).Wait();

                return Ok();
            }
            catch
            {
                throw new Exception("An error occurred while verifying the account.");
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePassword newPasswords)
        {
            try
            {
                // Validate null fields
                if (string.IsNullOrWhiteSpace(newPasswords.Password) || string.IsNullOrWhiteSpace(newPasswords.ConfirmPassword))
                {
                    return BadRequest("The password is required.");
                }

                if (string.IsNullOrWhiteSpace(newPasswords.Token))
                {
                    return BadRequest("Token not provided.");
                }

                // Validate if the passwords match
                if (newPasswords.Password != newPasswords.ConfirmPassword)
                {
                    return BadRequest("Passwords do not match.");
                }

                // Validate password length
                if (newPasswords.Password.Length < 8)
                {
                    return BadRequest("The password must be at least 8 characters long.");
                }

                var password = BCrypt.Net.BCrypt.HashPassword(newPasswords.Password);

                var user = await _context.Users.FirstOrDefaultAsync(x => x.Token == newPasswords.Token);

                // Validate token
                if (user == null)
                {
                    return BadRequest("Token not valid.");
                }

                user.Token = null;
                user.Password = password;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                throw new Exception("An error ocurred while changing password.");
            }
            
        }

        [HttpPost]
        [Route("Validate")]
        public async Task<IActionResult> ValidateToken(JwtToken token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token.Token))
                {
                    return BadRequest("Token not valid.");
                }

                if (!await Token.ValidateToken(_configuration, token.Token))
                {
                    return BadRequest("Token not valid.");
                }

                return Ok();
            }
            catch
            {
                return Unauthorized("Token not valid.");
            }
        }
    }
}
