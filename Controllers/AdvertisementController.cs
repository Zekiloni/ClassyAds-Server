using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyAds.Interfaces;
using MyAds.Entities;
using MyAds.Models;
using System.Net;
using ClassyAdsServer.Models;
using MyAds.Services;

namespace MyAds.Controllers
{
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IUserService _users;
        private readonly IAdvertisementMediaService _advertisementMedia;

        public AdvertisementController(IAdvertisementService advertisementService, IUserService users, IAdvertisementMediaService media)
        {
            _users = users;
            _advertisementService = advertisementService;
            _advertisementMedia = media;
        }

        [HttpGet("/advertisements/recent")]
        public async Task<IActionResult> GetRecentAdvertisements(int limit) {
            try
            {
                var advertisements = await _advertisementService.GetRecentAdvertisements(limit);
                return Ok(advertisements);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while retrieving recent advertisements.");
            }
        }

        [HttpPost("/advertisements/search")]
        public async Task<IActionResult> SearchAdvertisements(AdvertisementSearchInput searchAdvertisement)
        {
            var advertisements = await _advertisementService.GetAdvertisementsByFilter(searchAdvertisement.Filter, searchAdvertisement.CategoryId);

            var totalNumberOfRecords = advertisements.Count();
            var totalNumberOfPages = (int)Math.Ceiling((double)totalNumberOfRecords / searchAdvertisement.PageSize);

            var advertisementsCurrentPage = advertisements
                .Skip((searchAdvertisement.PageNumber - 1) * searchAdvertisement.PageSize)
                .Take(searchAdvertisement.PageSize);

            var pagedOutput = new PagedOutput<Advertisement>
            {
                PageNumber = searchAdvertisement.PageNumber,
                PageSize = searchAdvertisement.PageSize,
                TotalNumberOfPages = totalNumberOfPages,
                TotalNumberOfRecords = totalNumberOfRecords,
                Results = advertisementsCurrentPage
            };

            return StatusCode((int)HttpStatusCode.OK, pagedOutput);
        }

        [HttpGet("/advertisements/{advertisementId}")]
        public async Task<IActionResult> GetAdvertisementById(int advertisementId)
        {
            var advertisement = await _advertisementService.GetAdvertisementById(advertisementId);

            if (advertisement == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new ErrorResponse("Advertisement not found.", "Advertisement may be not active or is deleted."));
            }

            return Ok(advertisement);
        }

        [Authorize]
        [HttpPost("/advertisements/create")]
        public async Task<IActionResult> CreateAdvertisement(CreateAdvertisementInput newAdvertisement)
        {
            var user = _users.GetUserById((int)HttpContext.Items["UserId"]!);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null) { 
                return StatusCode((int)HttpStatusCode.Unauthorized, new ErrorResponse("You are not authorized.", null));
            }

            try
            {
                var advertisement = new Advertisement
                {
                    CategoryId = newAdvertisement.CategoryId,
                    Title = newAdvertisement.Title,
                    ShortDescription = newAdvertisement.ShortDescription,
                    Description = newAdvertisement.Description,
                    UserId = user.Id,
                    Status = Enums.AdvertisementStatus.Active,
                };

                if (advertisement == null)
                {
                    return BadRequest();
                }

                await _advertisementService.CreateAdvertisement(advertisement);

                if (newAdvertisement.MediaFiles != null)
                {
                    foreach (var file in newAdvertisement.MediaFiles)
                    {

                        var mediaFile = new AdvertisementMediaFile
                        {
                            AdvertisementId = advertisement.Id,
                            Url = await _advertisementMedia.UploadMediaFile(file)
                        };

                        if (mediaFile != null)
                        {
                            await _advertisementMedia.CreateMediaFile(mediaFile);
                        }
                    }
                }
    
                return Ok(advertisement);
            }
            catch (Exception errorCreating)
            {
                return BadRequest(errorCreating);
            }
        }

        [Authorize]
        [HttpDelete("/advertisements/delete/{advertisementId}")]
        public async Task<IActionResult> DeleteAdvertisement(int advertisementId)
        {
            var user = await _users.GetUserById((int)HttpContext.Items["UserId"]!);
            var advertisement = await _advertisementService.GetAdvertisementById(advertisementId);

            if (advertisement == null || user == null)
            {
                return NotFound();
            }

            if (advertisement.UserId != user.Id && user.Role < Enums.UserRole.Admin)
            {
                return Unauthorized();
            }

            await _advertisementService.DeleteAdvertisement(advertisement);

            return Ok();
        }
    }
}
