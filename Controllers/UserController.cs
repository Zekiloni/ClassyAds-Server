using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly Database database;

    public UserController(Database dbContext, IConfiguration config)
    {
        configuration = config;
        database = dbContext;
    }

    [HttpGet("/users/{id}")]
    [Authorize]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await database.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("/users/login")]
    public async Task<IActionResult> LoginUser(string username, string password)
    {
        var user = await database.Users.FirstOrDefaultAsync(user => user.Username == username);

        if (user == null)
        {
            return NotFound();
        }

        if (BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
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

            return Ok(new
            {
                user = user,
                token = tokenHandler.WriteToken(token)
            });
        }
        else
        {
            return Unauthorized();
        }
    }

    [HttpPost("/users/register")]
    public async Task<IActionResult> RegisterUser(string username, string email, string password, DateTime dateOfBirth)
    {
        var usernameAlreadyInUse = await database.Users.FirstOrDefaultAsync(user => user.Username == username) == null ? false : true;

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

        database.Users.Add(user);
        await database.SaveChangesAsync();

        // Create a JWT token for the newly registered user
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
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