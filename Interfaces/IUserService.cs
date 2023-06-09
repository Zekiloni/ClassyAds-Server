using MyAds.Entities;

namespace MyAds.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserById(int userId);

        Task<User?> GetUserByUsername(string username);

        Task<User?> GetUserByEmail(string email);

        Task<IEnumerable<User>> GetAllUsers();

        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(User user);
    }
}
