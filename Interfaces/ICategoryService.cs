using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category?> GetCategoryById(int categoryId);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
    }
}