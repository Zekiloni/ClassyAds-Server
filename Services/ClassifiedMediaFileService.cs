using MyAds.Entities;
using MyAds.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.EntityFrameworkCore;

namespace MyAds.Services
{
    public class ClassifiedMediaFileService : IClassifiedMediaService
    {
        private readonly Context _database;
        private readonly IWebHostEnvironment _env;

        public ClassifiedMediaFileService(Context database, IWebHostEnvironment environment)
        {
            _database = database;
            _env = environment;
        }

        public async Task CreateMediaFile(ClassifiedMediaFile mediaFile)
        {
            await _database.MediaFiles.AddAsync(mediaFile);
            await _database.SaveChangesAsync();
        }

        public Task DeleteMediaFile(int mediaFileId)
        {
            throw new NotImplementedException();
        }

        public async Task<ClassifiedMediaFile?> GetMediaFileById(int mediaFileId)
        {
            return await _database.MediaFiles.FirstOrDefaultAsync(m => m.Id == mediaFileId);
        }

        public Task<IEnumerable<ClassifiedMediaFile>> GetMediaFiles()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateMediaFile(ClassifiedMediaFile mediaFile)
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
            var filePath = Path.Combine(_env.ContentRootPath, "uploads/classifieds", newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
