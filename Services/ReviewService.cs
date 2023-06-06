using ClassyAdsServer.Interfaces;
using MyAds.Entities;

namespace ClassyAdsServer.Services
{
    public class ReviewService : IReviewService
    {

        private readonly DatabaseContext _database;

        public ReviewService(DatabaseContext database)
        {
            _database = database;
        }

        public async Task CreateReview(Review review)
        {
            await _database.Reviews.AddAsync(review);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteReview(Review review)
        {
            _database.Reviews.Remove(review);
            await _database.SaveChangesAsync();
        }

        public Task<Review> GetReviewById(int reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviewsByAdvertisementId(int advertisementId)
        {
            throw new NotImplementedException();
        }
    }
}
