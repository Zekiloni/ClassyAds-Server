using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId);
        Task<Notification> GetNotificationById(int notificationId);
        Task<int> GetUnreadNotificationCount(int userId);
        Task CreateNotification(Notification notification);
        Task MarkNotificationAsRead(int notificationId);
        Task DeleteNotification(int notificationId);
    }
}
