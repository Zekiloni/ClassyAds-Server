using Microsoft.EntityFrameworkCore;
using MyAds.Entities;
using MyAds.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyAds.Services
{
    public class ClassifiedService : IClassifiedService
    {
        private readonly DatabaseContext _database;

        public ClassifiedService(DatabaseContext database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Classified>> GetAllClassifieds()
        {
            return await _database.Classifieds.ToListAsync();
        }

        public async Task<IEnumerable<Classified>> GetClassifiedsByFilter(string filter, int? categoryId)
        {
            var classifieds = _database.Classifieds
                .Include(c => c.Category)
                .Include(c => c.User)
                .AsNoTracking();


            classifieds = classifieds.Where(c =>
                c.Title.Contains(filter) ||
                c.ShortDescription.Contains(filter) ||
                c.Description.Contains(filter) ||
                c.User!.Username.Contains(filter) ||
                c.User.EmailAddress != null && c.User.EmailAddress.Contains(filter)
            );

            if (categoryId != null)
            {
                classifieds = classifieds.Where(c => c.CategoryId == categoryId);
            }

            return await classifieds.ToListAsync();
        }

        public async Task<Classified?> GetClassifiedById(int classifiedId)
        {
            return await _database.Classifieds.FirstOrDefaultAsync(classified => classified.Id == classifiedId);
        }

        public async Task CreateClassified(Classified classified)
        {
            await _database.Classifieds.AddAsync(classified);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteClassified(Classified classified)
        {
            _database.Classifieds.Remove(classified);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateClassified(Classified order)
        {
            order.UpdatedAt = DateTime.Now;
            _database.Entry(order).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }
    }
}
