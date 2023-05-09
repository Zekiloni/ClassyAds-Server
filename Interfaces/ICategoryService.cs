using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(Category category);
    }
}