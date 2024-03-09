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

        [HttpPost("CreateAccount")]
        public async Task<ActionResult> CreateAccount(User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            user.Email = user.Email!.Trim();

            var existsUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            
            if(existsUser != null)
            {
                return BadRequest("This user already exists.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = passwordHash;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Created();
        }
    }
}
