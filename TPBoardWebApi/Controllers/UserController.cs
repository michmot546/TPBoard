using Microsoft.AspNetCore.Mvc;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TPBoardWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data");
            }

            if (_userService.UserExists(newUser.Login))
            {
                return Conflict("A user with this login already exists.");
            }

            if (_userService.EmailExists(newUser.Email))
            {
                return Conflict("A user with this email already exists.");
            }

            _userService.CreateUser(newUser);
            var token = GenerateJwtToken(newUser);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, new { token });
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto loginDto)
        {
            if (loginDto == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid login data");
            }

            if (_userService.VerifyUser(loginDto.Login, loginDto.Password))
            {
                var user = _userService.GetUserByLogin(loginDto.Login);
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            return BadRequest("Username or password is incorrect");
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [Authorize]
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("Mismatched user ID in the request");
            }

            _userService.UpdateUser(updatedUser);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(id);
            return NoContent();
        }

        [Authorize]
        [HttpGet("GetUserByName/{name}")]
        public IActionResult GetUserByName(string name)
        {
            var user = _userService.GetUserByName(name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login)
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
