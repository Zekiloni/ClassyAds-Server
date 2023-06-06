using MyAds.Entities;

namespace ClassyAdsServer.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetReviewsByAdvertisementId(int advertisementId);
        Task<Review> GetReviewById(int reviewId);
        Task CreateReview(Review review);
        Task DeleteReview(Review review);
    }
}
