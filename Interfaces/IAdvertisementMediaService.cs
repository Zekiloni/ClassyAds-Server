using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IAdvertisementMediaService
    {
        Task<IEnumerable<AdvertisementMediaFile>> GetMediaFiles();
        Task<AdvertisementMediaFile?> GetMediaFileById(int AdvertisementId);
        Task CreateMediaFile(AdvertisementMediaFile mediaFile);
        Task UpdateMediaFile(AdvertisementMediaFile mediaFile);
        Task DeleteMediaFile(AdvertisementMediaFile mediaFile);
        Task<string> UploadMediaFile(IFormFile file);
    }
}
