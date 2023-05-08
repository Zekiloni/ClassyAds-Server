using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IClassifiedService
    {
        Task<Classified?> GetClassifiedById(int classifiedId);
        Task<IEnumerable<Classified>> GetAllClassifieds();
        Task CreateClassified(Classified classified);
        Task UpdateClassified(Classified classified);
        Task DeleteClassified(Classified classified);
    }
}
