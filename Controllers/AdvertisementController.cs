using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAds.Interfaces;
using MyAds.Entities;
using MyAds.Models;


namespace MyAds.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAdvertisementService _Advertisements;
        private readonly IUserService _users;
        private readonly IAdvertisementMediaService _AdvertisementMedia;

        public AdvertisementController(IAdvertisementService Advertisements, IUserService users, IAdvertisementMediaService media, IConfiguration config)
        {
            _configuration = config;
            _users = users;
            _Advertisements = Advertisements;
            _AdvertisementMedia = media;
        }

        [HttpPost("/Advertisements/search")]
        public async Task<IActionResult> SearchAdvertisements(AdvertisementSearchInput searchAdvertisement)
        {
            var Advertisements = await _Advertisements.GetAdvertisementsByFilter(searchAdvertisement.Filter, searchAdvertisement.CategoryId);

            var totalNumberOfRecords = Advertisements.Count();
            var totalNumberOfPages = (int)Math.Ceiling((double)totalNumberOfRecords / searchAdvertisement.PageSize);

            var AdvertisementsCurrentPage = Advertisements
                .Skip((searchAdvertisement.PageNumber - 1) * searchAdvertisement.PageSize)
                .Take(searchAdvertisement.PageSize);

            var pagedOutput = new PagedOutput<Advertisement>
            {
                PageNumber = searchAdvertisement.PageNumber,
                PageSize = searchAdvertisement.PageSize,
                TotalNumberOfPages = totalNumberOfPages,
                TotalNumberOfRecords = totalNumberOfRecords,
                Results = AdvertisementsCurrentPage
            };

            return Ok(pagedOutput);
        }

        [HttpGet("/Advertisements/{AdvertisementId}")]
        [Authorize]
        public async Task<IActionResult> GetAdvertisementById(int AdvertisementId)
        {

            var Advertisement = await _Advertisements.GetAdvertisementById(AdvertisementId);

            if (Advertisement == null)
            {
                return NotFound();
            }

            return Ok(Advertisement);
        }

        [HttpPost("/Advertisements/create")]
        [Authorize]
        public async Task<IActionResult> CreateAdvertisement(NewAdvertisementInput newAdvertisement)
        {
            var userAuthorId = (int)HttpContext.Items["UserId"]!;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Advertisement = new Advertisement
                {
                    CategoryId = newAdvertisement.CategoryId,
                    Title = newAdvertisement.Title,
                    ShortDescription = newAdvertisement.ShortDescription,
                    Description = newAdvertisement.Description,
                    UserId = userAuthorId
                };

                if (Advertisement == null)
                {
                    return BadRequest();
                }

                await _Advertisements.CreateAdvertisement(Advertisement);

                foreach (var file in newAdvertisement.MediaFiles)
                {
                    var mediaFile = new AdvertisementMediaFile
                    {
                        AdvertisementId = Advertisement.Id,
                        Url = await _AdvertisementMedia.UploadMediaFile(file)
                    };

                    if (mediaFile != null)
                    {
                        await _AdvertisementMedia.CreateMediaFile(mediaFile);
                    }
                }
                return Ok(Advertisement);
            }
            catch (Exception errorCreating)
            {
                return BadRequest(errorCreating);
            }
        }


        [HttpDelete("Advertisements/delete/{AdvertisementId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAdvertisement(int AdvertisementId)
        {
            var user = await _users.GetUserById((int)HttpContext.Items["UserId"]!);
            var Advertisement = await _Advertisements.GetAdvertisementById(AdvertisementId);

            if (Advertisement == null || user == null)
            {
                return NotFound();
            }

            if (Advertisement.UserId != user.Id && user.Role < Enums.UserRole.Admin)
            {
                return Unauthorized();
            }

            await _Advertisements.DeleteAdvertisement(Advertisement);

            return Ok();
        }
    }
}
