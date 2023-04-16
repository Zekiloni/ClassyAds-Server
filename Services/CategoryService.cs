using MyAds.Entities;
using MyAds.Interfaces;

namespace MyAds.Services
{
    public class CategoryService : ICategoryService
    {
        public Task<Category> CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(int categoryId, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
