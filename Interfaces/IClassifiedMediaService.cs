using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IClassifiedMediaService
    {
        Task<IEnumerable<ClassifiedMediaFile>> GetMediaFiles();
        Task<ClassifiedMediaFile?> GetMediaFileById(int classifiedId);
        Task CreateMediaFile(ClassifiedMediaFile mediaFile);
        Task UpdateMediaFile(ClassifiedMediaFile mediaFile);
        Task DeleteMediaFile(ClassifiedMediaFile mediaFile);
        Task<string> UploadMediaFile(IFormFile file);
    }
}
