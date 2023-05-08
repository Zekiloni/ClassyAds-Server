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

        public Task<ClassifiedMediaFile> GetdMediaFileById(int categoryId)
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

        public async Task<string> SaveFileAsync(string classifiedTittle, IFormFile file)
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 8);
            var sanitizedTitle = classifiedTittle.Replace(' ', '_').Replace('-', '_');
            var newFileName = $"{sanitizedTitle}-{uniqueId}{fileExtension}";
            var filePath = Path.Combine(_env.ContentRootPath, "uploads/classifieds", newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
