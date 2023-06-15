using Microsoft.EntityFrameworkCore;
using ClassyAdsServer.Entities;
using ClassyAdsServer.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClassyAdsServer.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly DatabaseContext _database;

        public AdvertisementService(DatabaseContext database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisements()
        {
            return await _database.Advertisements.ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetAdvertisementsByFilter(string? filter, int? categoryId)
        {
            var advertisements = _database.Advertisements
                .Include(c => c.Category)
                .Include(c => c.User)
                .AsNoTracking();


            if (filter != null)
            {
                advertisements = advertisements.Where(c =>
                      c.Title.Contains(filter) ||
                      c.ShortDescription.Contains(filter) ||
                      c.Description.Contains(filter) ||
                      c.User!.Username.Contains(filter) ||
                      c.User.EmailAddress != null && c.User.EmailAddress.Contains(filter)
                  );
            }

            if (categoryId != null)
            {
                advertisements = advertisements.Where(c => c.CategoryId == categoryId);
            }

            return await advertisements.ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> GetRecentAdvertisements(int limit)
        {
            return await _database.Advertisements
                .Include(c => c.Category)
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedAt)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Advertisement?> GetAdvertisementById(int advertisementId)
        {
            return await _database.Advertisements.FirstOrDefaultAsync(advertisement => advertisement.Id == advertisementId);
        }

        public async Task CreateAdvertisement(Advertisement Advertisement)
        {
            await _database.Advertisements.AddAsync(Advertisement);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteAdvertisement(Advertisement Advertisement)
        {
            _database.Advertisements.Remove(Advertisement);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateAdvertisement(Advertisement order)
        {
            order.UpdatedAt = DateTime.Now;
            _database.Entry(order).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }
    }
}
