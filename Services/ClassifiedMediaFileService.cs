using MyAds.Entities;
using MyAds.Interfaces;

namespace MyAds.Services
{
    public class ClassifiedMediaFileService : IClassifiedMediaService
    {
        public Task<ClassifiedMediaFile> CreateMedieFile(ClassifiedMediaFile mediaFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMediaFile(int mediaFileId)
        {
            throw new NotImplementedException();
        }

        public Task<ClassifiedMediaFile> GetCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ClassifiedMediaFile>> GetMediaFiles()
        {
            throw new NotImplementedException();
        }

        public Task<ClassifiedMediaFile> UpdateMediaFile(int mediaFileId, ClassifiedMediaFile mediaFile)
        {
            throw new NotImplementedException();
        }
    }
}
