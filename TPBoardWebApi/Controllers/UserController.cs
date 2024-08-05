using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPBoardWebApi.Interfaces;
using TPBoardWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

            newUser.Role = "User";

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

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [Authorize(Roles = "Admin,Moderator,User")]
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

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("Mismatched user ID in the request");
            }

            if (User.IsInRole("User") && id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return Forbid();
            }

            _userService.UpdateUser(updatedUser);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin,Moderator,User")]
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
        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpGet("GetUserName/{id}")]
        public IActionResult GetUserName(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { name = user.Name });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUserName")]
        public IActionResult UpdateUserName([FromBody] User dto)
        {
            var user = _userService.GetUserById(dto.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = dto.Name;
            _userService.UpdateUser(user);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUserEmail")]
        public IActionResult UpdateUserEmail([FromBody] User dto)
        {
            var user = _userService.GetUserById(dto.Id);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = dto.Email;
            _userService.UpdateUser(user);

            return Ok();
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPut("UpdateUserNameSelf")]
        public IActionResult UpdateUserNameSelf([FromBody] UpdateNameDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId != dto.Id)
            {
                return Forbid();
            }

            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = dto.Name;
            _userService.UpdateUser(user);

            return Ok();
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPut("UpdateUserEmailSelf")]
        public IActionResult UpdateUserEmailSelf([FromBody] UpdateEmailDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (userId != dto.Id)
            {
                return Forbid();
            }

            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = dto.Email;
            _userService.UpdateUser(user);

            return Ok();
        }

        [Authorize(Roles = "Admin,Moderator,User")]
        [HttpPut("UpdateUserPassword")]
        public IActionResult UpdateUserPassword([FromBody] UpdatePasswordDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            if (!_userService.VerifyUser(user.Login, dto.OldPassword))
            {
                return BadRequest("Current password is incorrect.");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            _userService.UpdateUser(user);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("AssignModeratorRole/{userId}")]
        public IActionResult AssignModeratorRole(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Role = "Moderator";
            _userService.UpdateUser(user);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("RemoveModeratorRole/{userId}")]
        public IActionResult RemoveModeratorRole(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            user.Role = "User";
            _userService.UpdateUser(user);

            return Ok();
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
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Role, user.Role)
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
