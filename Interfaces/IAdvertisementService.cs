using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IAdvertisementService
    {
        Task<Advertisement?> GetAdvertisementById(int AdvertisementId);
        Task<IEnumerable<Advertisement>> GetAllAdvertisements();
        Task<IEnumerable<Advertisement>> GetAdvertisementsByFilter(string filter, int? categoryId);
        Task CreateAdvertisement(Advertisement Advertisement);
        Task UpdateAdvertisement(Advertisement Advertisement);
        Task DeleteAdvertisement(Advertisement Advertisement);
    }
}
