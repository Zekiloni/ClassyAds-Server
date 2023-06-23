
using ClassyAdsServer.Database;
using ClassyAdsServer.Entities;
using ClassyAdsServer.Interfaces;

namespace ClassyAdsServer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly DatabaseContext _database;

        public NotificationService(DatabaseContext database) { 
            _database = database; 
        }

        public Task CreateNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNotification(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetNotificationById(int notificationId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetUnreadNotificationCount(int userId)
        {
            throw new NotImplementedException();
        }

        public Task MarkNotificationAsRead(int notificationId)
        {
            throw new NotImplementedException();
        }
    }
}
