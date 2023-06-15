using ClassyAdsServer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ClassyAdsServer.Entities;

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

        public Task<IEnumerable<Review>> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public async Task<Review?> GetReviewById(int reviewId)
        {
            return await _database.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async Task<IEnumerable<Review>> GetReviewsByAdvertisementId(int advertisementId)
        {
            return await _database.Reviews
                .Where(r => r.AdvertisementId == advertisementId)
                .ToListAsync();
        }
    }
}
