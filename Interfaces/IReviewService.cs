using MyAds.Entities;

namespace ClassyAdsServer.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetReviewsByAdId(int adId);
        Task<Review> GetReviewById(int reviewId);
        Task CreateReview(Review review);
        Task UpdateReview(Review review);
        Task DeleteReview(Review review);
    }
}
