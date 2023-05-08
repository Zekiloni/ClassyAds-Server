using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IClassifiedMediaService
    {
        Task<IEnumerable<ClassifiedMediaFile>> GetMediaFiles();
        Task<ClassifiedMediaFile> GetCategoryById(int categoryId);
        Task<ClassifiedMediaFile> CreateMedieFile(ClassifiedMediaFile mediaFile);
        Task<ClassifiedMediaFile> UpdateMediaFile(int mediaFileId, ClassifiedMediaFile medieFile);
        Task DeleteMediaFile(int mediaFileId);
        Task<string> SaveFileAsync(ClassifiedMediaFile mediaFile);
    }
}
