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
        private readonly IClassifiedMediaService _classifiedMedia;

        public ClassifiedController(IClassifiedService classifieds, IClassifiedMediaService media, IConfiguration config)
        {
            _configuration = config;
            _classifieds = classifieds;
            _classifiedMedia = media;
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

            try
            {
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

                await _classifieds.CreateClassified(classified);

                foreach (var file in newClassified.MediaFiles)
                {
                    var mediaFile = new ClassifiedMediaFile
                    {
                        ClassifiedId = classified.Id,
                        Url = await _classifiedMedia.UploadMediaFile(file)
                    };

                    if (mediaFile != null)
                    {
                        await _classifiedMedia.CreateMediaFile(mediaFile);
                    }
                }
                return Ok(classified);
            }
            catch (Exception errorCreating)
            {
                return BadRequest(errorCreating);
            }
        }
    }
}
