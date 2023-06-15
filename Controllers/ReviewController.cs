
using ClassyAdsServer.Interfaces;
using ClassyAdsServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClassyAdsServer.Entities;
using ClassyAdsServer.Interfaces;
using System.Net;

namespace ClassyAdsServer.Controllers
{
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IAdvertisementService _advertisementService;

        public ReviewController(IReviewService reviewService, IUserService userService, IAdvertisementService advertisementService) 
        { 
            _reviewService = reviewService;
            _userService = userService;
            _advertisementService = advertisementService;
        }

        [HttpGet("/reviews")]
        public async Task<IActionResult> GetAllReviews() {
            var reviews = await _reviewService.GetAllReviews();

            return Ok(reviews);
        }

        [Authorize]
        [HttpPost("/reviews/create")]
        public async Task<IActionResult> CreateReview(CreateReviewInput newReview)
        {
            var loggedUser = await _userService.GetUserById((int)HttpContext.Items["UserId"]!);

            if (loggedUser == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var advertisement = await _advertisementService.GetAdvertisementById(newReview.AdvertisementId);

            if (advertisement == null)
            {
                return NotFound("Advertisement not found.");
            }

            var review = new Review
            {
                UserId = loggedUser.Id,
                Rating = newReview.Rating,
                Comment = newReview.Comment,
                AdvertisementId = advertisement.Id
            };

            await _reviewService.CreateReview(review);

            return Ok(review);
        }

        [Authorize]
        [HttpDelete("/reviews/delete")]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var loggedUser = await _userService.GetUserById((int)HttpContext.Items["UserId"]!);

            if (loggedUser == null)
            {
                return Unauthorized("LoggedUserNotFound");
            }

            var review = await _reviewService.GetReviewById(reviewId);

            if (review == null)
            {
                return NotFound("ReviewNotFound");
            }

            if (review.UserId != loggedUser.Id && loggedUser.IsAdmin == false)
            {
                return Unauthorized();
            }

            await _reviewService.DeleteReview(review);

            return Ok();
        }
    }
}
