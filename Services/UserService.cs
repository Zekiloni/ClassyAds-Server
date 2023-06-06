using Microsoft.EntityFrameworkCore;
using MyAds.Entities;
using MyAds.Interfaces;


namespace MyAds.Services
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _database;
        public UserService(DatabaseContext dbContext)
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
            user.UpdatedAt = DateTime.Now;
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

        public async Task<IEnumerable<Advertisement>?> GetUserAdvertisements(int userId)
        {
            var user = await _database.Users
                .Include(u => u.Advertisements)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.Advertisements;
        }
    }
}
