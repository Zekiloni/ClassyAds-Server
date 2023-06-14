using Microsoft.EntityFrameworkCore;
using MyAds.Entities;
using MyAds.Interfaces;

namespace MyAds.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext _database;

        public CategoryService(DatabaseContext dbContext)
        {
            _database = dbContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _database.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int categoryId)
        {
            return await _database.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

        }

        public async Task CreateCategory(Category category)
        {
            await _database.Categories.AddAsync(category);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _database.Categories.Remove(category);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            category.UpdatedAt = DateTime.Now;
            _database.Entry(category).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }
    }
}
