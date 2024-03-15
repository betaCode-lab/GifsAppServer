using GifsAppv2.Models;
using Microsoft.AspNetCore.Mvc;

namespace GifsAppv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        // Attributes
        private readonly GifsContext _context;

        // Constructor
        public RegisterController(GifsContext context)
        {
            _context = context;
        }

        // Actions

        [HttpPost]
        [Route("CreateAccount")]
        public async Task<ActionResult> CreateAccount(User user)
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

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Created();
            }
            catch
            {
                return StatusCode(500, new { Message = "An unexpected error occurred while creating the account. Please try again later." });  
            }
        }
    }
}
