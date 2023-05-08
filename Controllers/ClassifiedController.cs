using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAds.Interfaces;
using MyAds.Entities;
using MyAds.Models;

namespace MyAds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IClassifiedService _classifieds;

        public ClassifiedController(IClassifiedService classifieds, IConfiguration config)
        {
            _configuration = config;
            _classifieds = classifieds;
        }

        [HttpGet("/classifieds/{classifiedId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderByOd(int classifiedId)
        {

            var classified = await _classifieds.GetClassifiedById(classifiedId);

            if (classified == null)
            {
                return NotFound();
            }

            return Ok(classified);
        }

        [HttpPost("/classifieds/create")]
        [Authorize]
        public async Task<IActionResult> CreateClassified(NewClassifiedInput newClassified)
        {
            var userAuthorId = (int)HttpContext.Items["UserId"]!;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var classified = new Classified
            {
                CategoryId = newClassified.CategoryId,
                Title = newClassified.Title,
                ShortDescription = newClassified.ShortDescription,
                Description = newClassified.Description,
                UserId = userAuthorId
            };

            if (classified == null)
            {
                return BadRequest();
            }

            try
            {
                await _classifieds.CreateClassified(classified);

            }
            catch (Exception errorCreating)
            {
                return BadRequest(errorCreating);
            }


            return Ok(classified);
        }
    }
}
