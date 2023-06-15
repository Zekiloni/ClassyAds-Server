using ClassyAdsServer.Entities;

namespace ClassyAdsServer.Interfaces
{
    public interface IAdvertisementService
    {
        Task<Advertisement?> GetAdvertisementById(int advertisementId);

        Task<IEnumerable<Advertisement>> GetAllAdvertisements();

        Task<IEnumerable<Advertisement>> GetRecentAdvertisements(int limit);

        Task<IEnumerable<Advertisement>> GetAdvertisementsByFilter(string? filter, int? categoryId);

        Task CreateAdvertisement(Advertisement advertisement);

        Task UpdateAdvertisement(Advertisement advertisement);

        Task DeleteAdvertisement(Advertisement advertisement);
    }
}
