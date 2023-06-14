using ClassyAdsServer.Entities;
using ClassyAdsServer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClassyAdsServer.Services
{
    public class MessageService : IMessageService
    {
        private readonly DatabaseContext _database;

        public MessageService(DatabaseContext dbContext)
        {
            _database = dbContext;
        }

        public Task CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetAllMessages()
        {
            return await _database.Messages.ToListAsync();
        }

        public Task<Message> GetMessageById(int messageId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Message>> GetMessagesByUserId(int senderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
