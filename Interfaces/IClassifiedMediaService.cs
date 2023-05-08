using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IClassifiedMediaService
    {
        Task<IEnumerable<ClassifiedMediaFile>> GetMediaFiles();
        Task<ClassifiedMediaFile> GetdMediaFileById(int classifiedId);
        Task<ClassifiedMediaFile> CreateMedieFile(ClassifiedMediaFile mediaFile);
        Task<ClassifiedMediaFile> UpdateMediaFile(int mediaFileId, ClassifiedMediaFile medieFile);
        Task DeleteMediaFile(int mediaFileId);
        Task<string> SaveFileAsync(string classifiedTittle, IFormFile file);
    }
}
