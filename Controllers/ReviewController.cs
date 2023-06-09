
using ClassyAdsServer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyAds.Entities;

namespace ClassyAdsServer.Controllers
{
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService) { 
            _reviewService = reviewService;
        }


        [HttpGet("/reviews")]
        public async Task<IActionResult> GetAllReviews() {
            var reviews = await _reviewService.GetAllReviews();

            return Ok(reviews);
        }

        [HttpPost("/review")]
        public async Task<IActionResult> CreateReview()
        {
            return Ok(null);
        }
    }
}
