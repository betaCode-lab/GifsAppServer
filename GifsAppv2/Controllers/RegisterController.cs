using GifsAppv2.Models;
using GifsAppv2.Services.Email;
using GifsAppv2.Services.Token;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GifsAppv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        // Attributes
        private readonly GifsContext _context;
        private readonly IConfiguration _configuration;

        // Constructor
        public RegisterController(GifsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Actions
        [HttpPost]
        [Route("CreateAccount")]
        public async Task<ActionResult> CreateAccount(Models.User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (user.Password != user.ConfirmPassword)
                {
                    return BadRequest("Passwords do not match.");
                }

                user.Email = user.Email!.Trim();

                var existsUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);

                if (existsUser != null)
                {
                    return BadRequest("This user already exists.");
                }

                string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = passwordHash;

                // Create token
                //string token = Token.CreateToken(_configuration, user);
                //user.Token = token;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Send email
                // EmailService.CreateAccountEmail(_configuration, user.Email, token).Wait();

                return Created();
            }
            catch
            {
                return StatusCode(500, new { Message = "An unexpected error occurred while creating the account. Please try again later." });  
            }
        }

        
    }
}
