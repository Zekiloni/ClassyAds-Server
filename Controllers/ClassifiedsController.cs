using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly Database database;

        public ClassifiedsController(Database dbContext, IConfiguration config)
        {
            configuration = config;
            database = dbContext;
        }

        [HttpGet("/classifieds/{classifiedId}")]
        [Authorize]
        public async Task<IActionResult> GetClassifiedById(int classifiedId)
        {
            var classified = await database.Classifieds.FirstOrDefaultAsync(c => c.Id == classifiedId);

            if (classified == null)
            {
                return NotFound();
            }

            return Ok(classified);
        }

        [HttpPost("/classifieds/create")]
        [Authorize]
        public async Task<IActionResult> CreateClassified(int categoryId, string title, string shortDescription, string description)
        {

            var classified = new Classified
            {
                CategoryId = categoryId,
                Title = title,
                ShortDescription = shortDescription,
                Description = description
            };


            if (classified == null)
            {
                return BadRequest();
            }


            await database.SaveChangesAsync();

            return Ok(classified);
        }
    }
}
