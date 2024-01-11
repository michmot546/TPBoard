using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TPBoardWebApi.Data;

namespace TPBoardWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly TPBoardDbContext TPBoardDbContext;
        public UserController(TPBoardDbContext TPBoardDbContext)
        {
            this.TPBoardDbContext = TPBoardDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await this.TPBoardDbContext.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await this.TPBoardDbContext.Users.FindAsync(id);
            return Ok(user);
        }
    }
}
