using ClassyAdsServer.Entities;
using ClassyAdsServer.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using ClassyAdsServer.Database;

namespace ClassyAdsServer.Services
{
    public class AdvertisementMediaFileService : IAdvertisementMediaService
    {
        private readonly DatabaseContext _database;
        private readonly IWebHostEnvironment _env;

        public AdvertisementMediaFileService(DatabaseContext database, IWebHostEnvironment environment)
        {
            _database = database;
            _env = environment;
        }

        public async Task CreateMediaFile(AdvertisementMediaFile mediaFile)
        {
            await _database.MediaFiles.AddAsync(mediaFile);
            await _database.SaveChangesAsync();
        }

        public async Task DeleteMediaFile(AdvertisementMediaFile mediaFile)
        {
            _database.MediaFiles.Remove(mediaFile);
            await _database.SaveChangesAsync();
        }

        public async Task<AdvertisementMediaFile?> GetMediaFileById(int mediaFileId)
        {
            return await _database.MediaFiles.FirstOrDefaultAsync(m => m.Id == mediaFileId);
        }

        public Task<IEnumerable<AdvertisementMediaFile>> GetMediaFiles()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMediaFile(AdvertisementMediaFile mediaFile)
        {
            mediaFile.UpdatedAt = DateTime.Now;
            _database.Entry(mediaFile).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task<string> UploadMediaFile(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var uniqueId = Guid.NewGuid().ToString("N").Substring(0, 8);
            var newFileName = $"{uniqueId}{fileExtension}";
            var filePath = Path.Combine(_env.ContentRootPath, "uploads/Advertisements", newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
