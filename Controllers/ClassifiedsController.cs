using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAds.Interfaces;
using MyAds.Entities;

namespace MyAds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassifiedsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IClassifiedService _classifieds;

        public ClassifiedsController(IClassifiedService classifieds, IConfiguration config)
        {
            _configuration = config;
            _classifieds = classifieds;
        }

        [HttpGet("/classifieds/{classifiedId}")]
        [Authorize]
        public async Task<IActionResult> GetClassifiedById(int classifiedId)
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
