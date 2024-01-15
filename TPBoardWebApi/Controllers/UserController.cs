using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Data;
using TPBoardWebApi.Models;

namespace TPBoardWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly TPBoardDbContext _TPBoardDbContext;
        public UserController(TPBoardDbContext TPBoardDbContext)
        {
            _TPBoardDbContext = TPBoardDbContext;
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await this._TPBoardDbContext.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _TPBoardDbContext.Users.FindAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data");
            }

            _TPBoardDbContext.Users.Add(newUser);
            await _TPBoardDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("Mismatched user ID in the request");
            }

            _TPBoardDbContext.Entry(updatedUser).State = EntityState.Modified;

            try
            {
                await _TPBoardDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _TPBoardDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _TPBoardDbContext.Users.Remove(user);
            await _TPBoardDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _TPBoardDbContext.Users.Any(u => u.Id == id);
        }
    }
}
