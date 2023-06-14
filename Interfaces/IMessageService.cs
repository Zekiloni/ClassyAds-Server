using ClassyAdsServer.Entities;

namespace ClassyAdsServer.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetAllMessages();

        Task<Message> GetMessageById(int messageId);

        Task<List<Message>> GetMessagesByUserId(int senderId);

        Task CreateMessage(Message message);

        Task UpdateMessage(Message message);

        Task DeleteMessage(Message message);
    }
}
