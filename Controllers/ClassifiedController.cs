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
        private readonly IUserService _users;
        private readonly IClassifiedMediaService _classifiedMedia;

        public ClassifiedController(IClassifiedService classifieds, IUserService users, IClassifiedMediaService media, IConfiguration config)
        {
            _configuration = config;
            _users = users;
            _classifieds = classifieds;
            _classifiedMedia = media;
        }

        [HttpPost("/classifieds/search")]
        public async Task<IActionResult> SearchClassifieds(ClassifiedSearchInput searchClassified)
        {
            var classifieds = await _classifieds.GetClassifiedsByFilter(searchClassified.Filter, searchClassified.CategoryId);

            var totalNumberOfRecords = classifieds.Count();
            var totalNumberOfPages = (int)Math.Ceiling((double)totalNumberOfRecords / searchClassified.PageSize);

            var classifiedsCurrentPage = classifieds
                .Skip((searchClassified.PageNumber - 1) * searchClassified.PageSize)
                .Take(searchClassified.PageSize);

            var pagedOutput = new PagedOutput<Classified>
            {
                PageNumber = searchClassified.PageNumber,
                PageSize = searchClassified.PageSize,
                TotalNumberOfPages = totalNumberOfPages,
                TotalNumberOfRecords = totalNumberOfRecords,
                Results = classifiedsCurrentPage
            };

            return Ok(pagedOutput);
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


        [HttpDelete("classifieds/delete/{classifiedId}")]
        [Authorize]
        public async Task<IActionResult> DeleteClassified(int classifiedId)
        {
            var user = await _users.GetUserById((int)HttpContext.Items["UserId"]!);
            var classified = await _classifieds.GetClassifiedById(classifiedId);

            if (classified == null || user == null)
            {
                return NotFound();
            }

            if (classified.UserId != user.Id && user.Role < Enums.UserRole.Admin)
            {
                return Unauthorized();
            }

            await _classifieds.DeleteClassified(classified);

            return Ok();
        }
    }
}
