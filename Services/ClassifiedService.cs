using Microsoft.EntityFrameworkCore;
using MyAds.Entities;
using MyAds.Interfaces;

namespace MyAds.Services
{
    public class ClassifiedService : IClassifiedService
    {
        private readonly Context _database;

        public ClassifiedService(Context database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Classified>> GetAllClassifieds()
        {
            return await _database.Orders.ToListAsync();
        }

        public async Task<Classified?> GetClassifiedById(int classifiedId)
        {
            return await _database.Orders.FirstOrDefaultAsync(classified => classified.Id == classifiedId);
        }

        public async Task CreateClassified(Classified classified)
        {
            await _database.Orders.AddAsync(classified);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteClassified(Classified classified)
        {
            _database.Orders.Remove(classified);
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
