using Microsoft.EntityFrameworkCore;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class UserService : IUserService
    {
        private readonly Database _database;

        public UserService(Database dbContext)
        {
            _database = dbContext;
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _database.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _database.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task CreateUser(User user)
        {
            await _database.Users.AddAsync(user);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _database.Entry(user).State = EntityState.Modified;
            await _database.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _database.Users.Remove(user);
            await _database.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _database.Users.ToListAsync();
        }
    }
}
