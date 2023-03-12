using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Database database;

        public UserController(Database dbContext)
        {
            database = dbContext;
        }

        [HttpGet("/users/{id}")]
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
            var user =  await database.Users.FirstOrDefaultAsync(user => user.Username == username);

            if (user == null)
            {
                return NotFound();
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
            {
                return Ok(user);
            } else 
            {
                return Unauthorized();
            }
        }
    }
}
