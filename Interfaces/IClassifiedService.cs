using WebApplication1.Models;

namespace WebApplication1.Interfaces
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
