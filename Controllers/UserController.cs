using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAds.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyAds.Entities;
using MyAds.Services;
using MyAds.Middlewares;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly UserAuthentication _userAuthentication;

    public UserController(IUserService userService, IConfiguration config, UserAuthentication userAuth)
    {
        _configuration = config;
        _userService = userService;
        _userAuthentication = userAuth;

    }

    [HttpGet("/users/{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        Console.WriteLine("getUserById http");

        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("/users/login")]
    public async Task<IActionResult> LoginUser(string username, string password)
    {
        var user = await _userService.GetUserByUsername(username);

        if (user == null)
        {
            return NotFound();
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
        {
            user.LastLoginAt = DateTime.Now;
            await _userService.UpdateUser(user);

            string token = _userAuthentication.CreateUserToken(user);

            return Ok(new
            {
                user,
                token
            });
        }
        else
        {
            return Unauthorized("Invalid password !");
        }
    }

    [HttpPost("/users/register")]
    public async Task<IActionResult> RegisterUser(string username, string email, string password, DateTime dateOfBirth)
    {
        var usernameAlreadyInUse = await _userService.GetUserByUsername(username) != null;

        if (usernameAlreadyInUse)
        {
            return BadRequest("Username already in use.");
        }

        var user = new User
        {
            Username = username,
            EmailAddress = email,
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(password),
            DateOfBirth = dateOfBirth
        };

        await _userService.CreateUser(user);

        // Create a JWT token for the newly registered user
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.EmailAddress)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        // return a 201 Created response with the newly created user object and the JWT token
        return CreatedAtAction(nameof(GetUserById), new
        {
            id = user.Id,
            user = user,
            token = tokenHandler.WriteToken(token)
        });
    }
}