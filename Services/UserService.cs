using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly Database database;

        public UserService(Database dbContext)
        {
            database = dbContext;
        }

        public async Task<User> GetUserById(string userId)
        {
            return await database.Users.FirstOrDefaultAsync(u => u.Id == int.Parse(userId));
        }

        public async Task CreateUser(User user)
        {
            await database.Users.AddAsync(user);
            await database.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            database.Entry(user).State = EntityState.Modified;
            await database.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            database.Users.Remove(user);
            await database.SaveChangesAsync();
        }

        public Task<User> GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
