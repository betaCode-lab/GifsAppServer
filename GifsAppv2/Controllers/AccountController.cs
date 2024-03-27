using GifsAppv2.Models;
using GifsAppv2.Models.OPT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GifsAppv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly GifsContext _context;

        public AccountController(GifsContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        [Route("GetUserById/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Id not valid.");
                }

                var user = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = new UserDTO()
                {
                    IdUser = user.IdUser,
                    Username = user.Username,
                    Email = user.Email,
                };

                return Ok(userDto);
            }
            catch
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, UserDTO user) 
        {
            try
            {
                if(id != user.IdUser)
                {
                    return BadRequest("The id is not valid.");
                }

                var userFound = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);

                if(userFound == null)
                {
                    return NotFound("User not found.");
                }

                // Update columns
                userFound.Username = user.Username;
                userFound.Email = user.Email;

                _context.Entry(userFound).State = EntityState.Modified;
                _context.SaveChanges();

                return NoContent();
            }
            catch
            {
                throw;
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("The id is not valid.");
                }

                var user = await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);

                if(user == null)
                {
                    return NotFound("User not found.");
                }

                _context.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch
            {
                throw;
            }
        }
    }
}
