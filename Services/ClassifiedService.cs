using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class ClassifiedService : IClassifiedService
    {
        private readonly Database _database;
        public ClassifiedService(Database database)
        {
            _database = database;
        }
        public async Task<IEnumerable<Classified>> GetAllClassifieds()
        {
            return await _database.Classifieds.ToListAsync();
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

        public Task UpdateClassified(Classified classified)
        {
            throw new NotImplementedException();
        }
    }
}
