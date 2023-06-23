using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using ClassyAdsServer.Models;
using ClassyAdsServer.Interfaces;
using ClassyAdsServer.Entities;


namespace ClassyAdsServer.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _users;

        public UserController(IUserService userService, IConfiguration config)
        {
            _configuration = config;
            _users = userService;
        }

        private string CreateUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration.GetValue<string>("Jwt:Secret");

            if (secretKey != null)
            {
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            throw new Exception("Secret JWT key is not defined in appsettings.json");
        }

        [HttpPost("/users/login")]
        public async Task<IActionResult> LoginUser(LoginUserInput loginUser)
        {
            Console.WriteLine("Username is " + loginUser.Username);
            var user = await _users.GetUserByUsername(loginUser.Username);
            Console.WriteLine("before user is null check");

            if (user == null)
            {
                Console.WriteLine("user is null");
                return StatusCode((int)HttpStatusCode.NotFound, new ErrorResponse("Username not found.", null));
            }

            Console.WriteLine("yees");

            if (BCrypt.Net.BCrypt.Verify(loginUser.Password, user.HashedPassword))
            {
                user.LastLoginAt = DateTime.Now;
                await _users.UpdateUser(user);
                Console.WriteLine("yees 2");

                try
                {
                    var token = CreateUserToken(user);

                    return Ok(new
                    {
                        user,
                        token
                    });
                }
                catch (Exception error)
                {
                    Console.Write(error);
                    return BadRequest();
                }
            }
            else
            {
                Console.WriteLine("yees but no");

                return StatusCode((int)HttpStatusCode.Unauthorized, new ErrorResponse("Incorrect password.", null));
            }
        }

        [HttpPost("/users/register")]
        public async Task<IActionResult> RegisterUser(RegisterUserInput newUser)
        {
            var usernameAlreadyInUse = await _users.GetUserByUsername(newUser.Username) != null;

            Console.Write(newUser);

            if (usernameAlreadyInUse)
            {
                return BadRequest("Username already in use.");
            }

            var user = new User
            {
                Username = newUser.Username,
                EmailAddress = newUser.EmailAddress,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password),
                DateOfBirth = newUser.DateOfBirth
            };

            await _users.CreateUser(user);

            return Ok(new
            {
                user
            });
        }

        [HttpGet("/users/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _users.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
